using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Models.ViewModels;

namespace Team24_BevosBooks.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(AppDbContext context,
                                 UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ================================
        // STATE LIST (U.S. Abbreviations)
        // ================================
        private List<string> GetStates()
        {
            return new List<string>
            {
                "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
                "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
                "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
                "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
                "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
            };
        }

        // ================================
        // REGISTER (GET)
        // ================================
        public IActionResult Register()
        {
            ViewBag.States = GetStates();
            return View();
        }

        // ================================
        // REGISTER (POST)
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            ViewBag.States = GetStates();

            if (!ModelState.IsValid)
                return View(rvm);

            AppUser newUser = new AppUser
            {
                UserName = rvm.Email,
                Email = rvm.Email,
                FirstName = rvm.FirstName,
                LastName = rvm.LastName,
                Address = rvm.Address,
                City = rvm.City,
                State = rvm.State,
                ZipCode = rvm.ZipCode,
                PhoneNumber = rvm.PhoneNumber,
                Status = AppUser.UserStatus.Customer
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, rvm.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(rvm);
            }

            await _userManager.AddToRoleAsync(newUser, "Customer");
            await _signInManager.SignInAsync(newUser, isPersistent: false);

            TempData["Message"] = "Account created successfully!";
            return RedirectToAction("Index", "Home");
        }

        // ================================
        // LOGIN (GET)
        // ================================
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnURL = returnUrl;
            return View();
        }

        // ================================
        // LOGIN (POST)
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lvm, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(lvm);

            var result = await _signInManager.PasswordSignInAsync(
                lvm.Email, lvm.Password, lvm.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(lvm);
            }

            AppUser user = await _userManager.Users.FirstAsync(u => u.Email == lvm.Email);

            if (user.Status == AppUser.UserStatus.Disabled)
            {
                await _signInManager.SignOutAsync();
                ModelState.AddModelError("", "Your account has been disabled.");
                return View(lvm);
            }

            if (!String.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // ================================
        // LOGOUT
        // ================================
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Message"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        // ================================
        // MANAGE PROFILE (GET)
        // ================================
        public async Task<IActionResult> Manage()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            ViewBag.States = GetStates();
            return View(user);
        }

        // ================================
        // MANAGE PROFILE (POST)
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(AppUser updatedUser)
        {
            ViewBag.States = GetStates();

            AppUser user = await _userManager.GetUserAsync(User);

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Address = updatedUser.Address;
            user.City = updatedUser.City;
            user.State = updatedUser.State;
            user.ZipCode = updatedUser.ZipCode;
            user.PhoneNumber = updatedUser.PhoneNumber;

            await _userManager.UpdateAsync(user);

            TempData["Message"] = "Profile updated successfully.";
            return RedirectToAction("Manage");
        }

        // ================================
        // CHANGE PASSWORD
        // ================================
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel cpvm)
        {
            if (!ModelState.IsValid)
                return View(cpvm);

            AppUser user = await _userManager.GetUserAsync(User);

            var result = await _userManager.ChangePasswordAsync(
                user, cpvm.OldPassword, cpvm.NewPassword);

            if (!result.Succeeded)
            {
                foreach (IdentityError e in result.Errors)
                    ModelState.AddModelError("", e.Description);

                return View(cpvm);
            }

            await _signInManager.RefreshSignInAsync(user);

            TempData["Message"] = "Password changed successfully.";
            return RedirectToAction("Manage");
        }

        // ================================
        // ADD CREDIT CARD
        // ================================
        public IActionResult AddCard()
        {
            return View();
        }

        // in namespace Team24_BevosBooks.Controllers
        // inside public class AccountController : Controller
        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCard(CardViewModel cvm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            // Build a customer name for the card (matches NOT NULL column in Cards table)
            // Build a customer name for the card
            string customerName = $"{user.FirstName} {user.LastName}";

            Card newCard = new Card
            {
                CardType = cvm.CardType,
                CardNumber = cvm.CardNumber,
                UserID = user.Id,
                CustomerName = customerName
            };

            _context.Cards.Add(newCard);
            await _context.SaveChangesAsync();

            // Send them back to checkout
            return RedirectToAction("Checkout", "Orders");

            // send them back to checkout (or wherever you want)
            return RedirectToAction("Checkout", "Orders");
        }
    }
}