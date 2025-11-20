using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedRoles
    {
        private static readonly string[] Roles = new[]
        {
            "Customer",
            "Employee",
            "Admin"
        };

        public static async Task AddAllRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (string roleName in Roles)
            {
                // If role doesn't exist, create it
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    IdentityRole role = new IdentityRole { Name = roleName };

                    IdentityResult result = await roleManager.CreateAsync(role);

                    if (!result.Succeeded)
                    {
                        throw new Exception(
                            $"Error creating role '{roleName}': " +
                            string.Join("; ", result.Errors)
                        );
                    }
                }
            }
        }
    }
}
