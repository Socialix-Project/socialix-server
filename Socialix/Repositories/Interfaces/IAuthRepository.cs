using Socialix.Entities;

namespace Socialix.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(User user);
    }
}
