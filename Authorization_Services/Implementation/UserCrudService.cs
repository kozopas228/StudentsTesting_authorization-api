using Authorization_Data.Interfaces;
using Authorization_Models;
using Authorization_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization_Services.Implementation
{
    public class UserCrudService : IUserCrudService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestAttemptRepository _testAttemptRepository;

        public UserCrudService(IUserRepository userRepository, ITestAttemptRepository testAttemptRepository)
        {
            _userRepository = userRepository;
            _testAttemptRepository = testAttemptRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(User user)
        {
            user.Password = Hashing.GetHashString(user.Password);
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task CreateAsync(User user)
        {
            user.Password = Hashing.GetHashString(user.Password);
            await _userRepository.CreateAsync(user);
        }
    }
}