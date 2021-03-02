using System.Threading.Tasks;
using Authorization_Models;

namespace Authorization_Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Login(string login, string password);
        Task<bool> Register(User user);
        string GenerateJWT(User user);
    }
}