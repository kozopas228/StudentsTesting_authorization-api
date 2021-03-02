using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization_Data.Interfaces;
using Authorization_Models;
using Authorization_Services.Interfaces;

namespace Authorization_Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestAttemptRepository _tempAttemptRepository;

        public UserService(IUserRepository userRepository, ITestAttemptRepository tempAttemptRepository)
        {
            _userRepository = userRepository;
            _tempAttemptRepository = tempAttemptRepository;
        }

        public async Task ChangeRole(Guid userId, string role)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var user = allUsers.First(x => x.Id == userId);

            user.Role = role;

            await _userRepository.UpdateAsync(user);
        }

        public async Task AddUserAttempt(Guid userId, Guid testId)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var user = allUsers.First(x => x.Id == userId);

            var attempt = new TestAttempt{TestGrade = 0, TestId = testId, TestState = TestState.NotDone, UserId = user.Id};

            await _tempAttemptRepository.CreateAsync(attempt);

        }

        public async Task<bool> ChangeUserLogin(Guid userId, string newLogin)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var user = allUsers.First(x => x.Id == userId);

            if (newLogin.Length < 4)
            {
                return false;
            }

            user.Login = newLogin;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ChangeUserPassword(Guid userId, string newPassword)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var user = allUsers.First(x => x.Id == userId);

            if (newPassword.Length < 4)
            {
                return false;
            }

            user.Password = newPassword;

            await _userRepository.UpdateAsync(user);
            return true;
        }
        public async Task<bool> ChangeUserFirstName(Guid userId, string newFirstName)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var user = allUsers.First(x => x.Id == userId);

            if (newFirstName.Length < 4)
            {
                return false;
            }

            user.FirstName = newFirstName;

            await _userRepository.UpdateAsync(user);
            return true;
        }
        public async Task<bool> ChangeUserLastName(Guid userId, string newLastName)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var user = allUsers.First(x => x.Id == userId);

            if (newLastName.Length < 4)
            {
                return false;
            }

            user.LastName = newLastName;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<Guid> GetUserId(string login)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var user = allUsers.First(x => x.Login == login);

            return user.Id;
        }

        public async Task<IEnumerable<TestAttempt>> GetAttemptsById(Guid userId)
        {
            var allAttempts = await _tempAttemptRepository.GetAllAsync();

            var result = allAttempts.Where(x => x.UserId == userId);

            return result;
        }
    }
}