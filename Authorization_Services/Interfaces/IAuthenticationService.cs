using Authorization_Models;
using System.Threading.Tasks;

namespace Authorization_Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Login(string login, string password);
        Task<bool> Register(User user);
        string GenerateJWT(User user);
    }
}