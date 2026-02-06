using Hospital_Management.Api.Models.Auth;

namespace Hospital_Management.Api.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> UserExistsByEmailAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}
