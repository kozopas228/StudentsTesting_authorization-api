using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization_Models;

namespace Authorization_Services.Interfaces
{
    public interface ITestAttemptCrudService
    {
        Task<IEnumerable<TestAttempt>> GetAllAsync();
        Task<bool> UpdateAsync(TestAttempt testAttempt);
        Task<bool> DeleteAsync(Guid id);
        Task CreateAsync(TestAttempt testAttempt);
    }
}