using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization_Data.Interfaces;
using Authorization_Models;
using Microsoft.EntityFrameworkCore;

namespace Authorization_Data.Implementation
{
    public class TestAttemptRepository : ITestAttemptRepository
    {
        private readonly ApplicationContext _context;

        public TestAttemptRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TestAttempt>> GetAllAsync()
        {
            return await _context.TestAttempts.ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var testAttempt = await _context.TestAttempts.FindAsync(id);
            if (testAttempt == null)
            {
                return false;
            }

            _context.TestAttempts.Remove(testAttempt);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(TestAttempt obj)
        {
            var testAttempt = await _context.TestAttempts.FindAsync(obj.Id);
            if (testAttempt == null)
            {
                return false;
            }

            testAttempt.TestGrade = obj.TestGrade;
            testAttempt.TestId = obj.TestId;
            testAttempt.TestState = obj.TestState;
            testAttempt.UserId = obj.UserId;

            _context.TestAttempts.Update(testAttempt);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task CreateAsync(TestAttempt obj)
        {
            await _context.TestAttempts.AddAsync(obj);
            await _context.SaveChangesAsync();

        }
    }
}