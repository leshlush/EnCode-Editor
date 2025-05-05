using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Helpers
{
    public class UserHelper
    {
        private readonly MongoDbContext _mongoDbContext;
        private readonly UserManager<AppUser> _userManager;

        public UserHelper(MongoDbContext mongoDbContext, UserManager<AppUser> userManager)
        {
            _mongoDbContext = mongoDbContext;
            _userManager = userManager;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateUserAsync(
    string email,
    string password,
    string firstName,
    string lastName,
    string role)
        {
            try
            {
                // Step 1: Create user in MongoDB
                var mongoUser = new User
                {
                    Username = email,
                    Email = email,
                    CreatedAt = DateTime.UtcNow
                };
                await _mongoDbContext.Users.InsertOneAsync(mongoUser);

                // Step 2: Create user in MySQL
                var appUser = new AppUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    MongoUserId = mongoUser.Id, // Use the MongoDB ID
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(appUser, password);
                if (!result.Succeeded)
                {
                    // Rollback MongoDB user if MySQL user creation fails
                    await _mongoDbContext.Users.DeleteOneAsync(u => u.Id == mongoUser.Id);
                    return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                // Step 3: Assign role to the user using UserManager
                var roleResult = await _userManager.AddToRoleAsync(appUser, role);
                if (!roleResult.Succeeded)
                {
                    return (false, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }

                return (true, string.Empty); // Updated to avoid nullability issues
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

    }
}
