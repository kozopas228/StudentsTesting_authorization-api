using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization_Data.Interfaces;
using Authorization_Models;
using Microsoft.EntityFrameworkCore;

namespace Authorization_Data.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(User obj)
        {
            var user = await _context.Users.FindAsync(obj.Id);
            if (user == null)
            {
                return false;
            }

            user.FirstName = obj.FirstName;
            user.LastName = obj.LastName;
            user.Login = obj.Login;
            user.Password = obj.Password;
            user.Role = obj.Role;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task CreateAsync(User obj)
        {
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();

        }
    }
}