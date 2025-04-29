using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SnapSaves.Auth;
using System.Security.Claims;

namespace SnapSaves.Auth
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            // Add the MongoUserId claim
            if (!string.IsNullOrEmpty(user.MongoUserId))
            {
                identity.AddClaim(new Claim("MongoUserId", user.MongoUserId));
            }

            return identity;
        }
    }
}