using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization_Models;

namespace Authorization_Services.Interfaces
{
    public interface IUserCrudService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
        Task CreateAsync(User user);
    }
}