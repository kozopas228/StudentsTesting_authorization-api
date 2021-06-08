using Authorization_Data.Interfaces;
using Authorization_Models;
using Authorization_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization_Services.Implementation
{
    public class TestAttemptCrudService : ITestAttemptCrudService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestAttemptRepository _testAttemptRepository;

        public TestAttemptCrudService(IUserRepository userRepository, ITestAttemptRepository testAttemptRepository)
        {
            _userRepository = userRepository;
            _testAttemptRepository = testAttemptRepository;
        }
        public async Task<IEnumerable<TestAttempt>> GetAllAsync()
        {
            return await _testAttemptRepository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(TestAttempt testAttempt)
        {
            return await _testAttemptRepository.UpdateAsync(testAttempt);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _testAttemptRepository.DeleteAsync(id);
        }

        public async Task CreateAsync(TestAttempt testAttempt)
        {
            await _testAttemptRepository.CreateAsync(testAttempt);
        }
    }
}