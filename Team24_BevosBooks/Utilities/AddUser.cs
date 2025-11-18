using Microsoft.AspNetCore.Identity;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using System.Text;

namespace Team24_BevosBooks.Utilities
{
    public static class AddUser
    {
        public async static Task<IdentityResult> AddUserWithRoleAsync(AddUserModel aum, UserManager<AppUser> userManager, AppDbContext _context)
        {
            AppUser dbUser = await userManager.FindByEmailAsync(aum.User.Email);
            IdentityResult result;

            if (dbUser == null)
            {
                try
                {
                    result = await userManager.CreateAsync(aum.User, aum.Password);
                }
                catch (Exception ex)
                {
                    StringBuilder msg = new StringBuilder();
                    msg.Append("There was an error adding the user with the email ");
                    msg.Append(aum.User.Email);
                    msg.Append(". Are you missing a required field on AppUser?");
                    throw new Exception(msg.ToString(), ex);
                }

                if (!result.Succeeded)
                {
                    StringBuilder msg = new StringBuilder();
                    foreach (var error in result.Errors) msg.AppendLine(error.Description);
                    throw new Exception("This user can't be added: " + msg.ToString());
                }

                dbUser = await userManager.FindByEmailAsync(aum.User.Email);
            }
            else
            {
                // update fields (not email/username)
                dbUser.PhoneNumber = aum.User.PhoneNumber;
                dbUser.FirstName = aum.User.FirstName;
                dbUser.LastName = aum.User.LastName;

                _context.Update(dbUser);
                _context.SaveChanges();

                var token = await userManager.GeneratePasswordResetTokenAsync(dbUser);
                result = await userManager.ResetPasswordAsync(dbUser, token, aum.Password);
            }

            if (!await userManager.IsInRoleAsync(dbUser, aum.RoleName))
            {
                await userManager.AddToRoleAsync(dbUser, aum.RoleName);
            }

            return result;
        }
    }

    public class AddUserModel
    {
        public AppUser User { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
