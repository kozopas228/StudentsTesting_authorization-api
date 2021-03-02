using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization_Models;

namespace Authorization_Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserToRole(Guid userId, string role);
        Task RemoveUserFromRole(Guid userId, string role);
        Task AddUserAttempt(Guid userId, Guid testId);
        Task<bool> ChangeUserLogin(Guid userId, string newLogin);
        Task<bool> ChangeUserPassword(Guid userId, string newPassword);
        Task<bool> ChangeUserFirstName(Guid userId, string newFirstName);
        Task<bool> ChangeUserLastName(Guid userId, string newLastName);
        Task<Guid> GetUserId(string login);
        Task<IEnumerable<TestAttempt>> GetAttemptsById(Guid userId);
    }
}