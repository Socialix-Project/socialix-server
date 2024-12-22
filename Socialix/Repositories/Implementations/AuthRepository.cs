using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Socialix.Data;
using Socialix.Entities;
using Socialix.Repositories.Interfaces;

namespace Socialix.Repositories.Implementations
{
    /// <summary>
    /// AuthRepository
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private UserManager<IdentityUser> _userManager;
        private AuthDbContext _authDbContext;
        private ApplicationDbContext _applicationDbContext;

        public AuthRepository(UserManager<IdentityUser> userManager, AuthDbContext authDbContext, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _authDbContext = authDbContext;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var userInAuthDb = await _userManager.FindByNameAsync(username);
            if (userInAuthDb == null) return false;


            var isSuccceded = _userManager.PasswordHasher.VerifyHashedPassword(userInAuthDb, userInAuthDb.PasswordHash, password);
            if (isSuccceded == PasswordVerificationResult.Failed) return false;

            var providedPassword = _userManager.PasswordHasher.HashPassword(userInAuthDb, password);
            var userInAppDb = await _applicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == username && x.PasswordHash == providedPassword);

            return userInAppDb != null;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Id = user.Id,
            };

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(identityUser, user.PasswordHash);
            identityUser.PasswordHash = user.PasswordHash;
            var role = await _authDbContext.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "User");

            await _userManager.CreateAsync(identityUser);
            await _userManager.AddToRoleAsync(identityUser, role?.Name ?? "User");
            await _applicationDbContext.Users.AddAsync(user);

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
