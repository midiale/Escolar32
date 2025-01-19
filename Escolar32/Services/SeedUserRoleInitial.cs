using Microsoft.AspNetCore.Identity;

namespace Escolar32.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, 
               RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("usuario@localhost.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost.com";
                user.Email = "usuario@localhost.com";
                user.NormalizedUserName = "USUARIO@LOCALHOST.COM";
                user.NormalizedEmail = "USUARIO@LOCALHOST.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2023").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("admin@localhost.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost.com";
                user.Email = "admin@localhost.com";
                user.NormalizedUserName = "ADMIN@LOCALHOST.COM";
                user.NormalizedEmail = "ADMIN@LOCALHOST.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2023").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}