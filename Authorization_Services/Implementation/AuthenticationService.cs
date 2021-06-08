using Authorization_Data.Interfaces;
using Authorization_Models;
using Authorization_Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authorization_Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestAttemptRepository _tempAttemptRepository;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthenticationService(IUserRepository userRepository, ITestAttemptRepository tempAttemptRepository, IOptions<AuthOptions> authOptions)
        {
            _userRepository = userRepository;
            _tempAttemptRepository = tempAttemptRepository;
            _authOptions = authOptions;
        }
        public async Task<string> Login(string login, string password)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var user = allUsers.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (user == null)
            {
                return "";
            }

            return GenerateJWT(user);
        }

        public async Task<bool> Register(User user)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var foundUser = allUsers.FirstOrDefault(x => x.Login == user.Login);

            if (foundUser != null)
            {
                return false;
            }

            if (user.Login.Length < 4 || user.Password.Length < 4)
            {
                return false;
            }

            user.Role = "user";

            await _userRepository.CreateAsync(user);
            return true;
        }

        public string GenerateJWT(User user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.Login),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role", user.Role)
            };

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}