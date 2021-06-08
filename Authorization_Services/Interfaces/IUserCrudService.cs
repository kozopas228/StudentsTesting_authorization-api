using Authorization_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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