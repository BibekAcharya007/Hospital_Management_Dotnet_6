using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.Models.Auth;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly HospitalDbContext _context;

        public AuthRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
            => await _context.Users.AnyAsync(u => u.Email == email);

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
