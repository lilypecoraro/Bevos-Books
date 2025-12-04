using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Models.ViewModels;
using Team24_BevosBooks.Services;

namespace Team24_BevosBooks.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(AppDbContext context,
                                 UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // ============================================================
        // STATES
        // ============================================================
        private List<string> GetStates() => new()
        {
            "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
        };

        // ============================================================
        // REGISTER (GET)
        // ============================================================
        public IActionResult Register()
        {
            ViewBag.States = GetStates();
            return View();
        }

        // ============================================================
        // REGISTER (POST)
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            ViewBag.States = GetStates();

            if (!ModelState.IsValid)
                return View(rvm);

            AppUser newUser = new()
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
                    ModelState.AddModelError("", error.Description);

                return View(rvm);
            }

            await _userManager.AddToRoleAsync(newUser, "Customer");
            await _signInManager.SignInAsync(newUser, false);

            // EMAIL — Account Created
            await _emailSender.SendEmailAsync(
                newUser.Email,
                "Team 24: Welcome to Bevo's Books!",
                EmailTemplate.Wrap($@"
                    <h2>Welcome, {newUser.FirstName}!</h2>
                    <p>Your Bevo's Books account has been successfully created.</p>
                    <p>We're excited to have you 🤘</p>
                ")
            );

            TempData["Message"] = "Account created successfully!";
            return RedirectToAction("Index", "Home");
        }

        // ============================================================
        // LOGIN
        // ============================================================
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnURL = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lvm, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(lvm);

            var result = await _signInManager.PasswordSignInAsync(
                lvm.Email, lvm.Password, lvm.RememberMe, false);

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

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // ============================================================
        // LOGOUT
        // ============================================================
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Message"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        // ============================================================
        // MANAGE PROFILE (GET)
        // ============================================================
        public async Task<IActionResult> Manage()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            ViewBag.States = GetStates();
            return View(user);
        }

        // ============================================================
        // MANAGE PROFILE (POST)
        // ============================================================
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(AppUser updatedUser)
        {
            ViewBag.States = GetStates();

            AppUser user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            // Remove Identity-only fields so validation passes
            ModelState.Remove(nameof(AppUser.Id));
            ModelState.Remove(nameof(AppUser.UserName));
            ModelState.Remove(nameof(AppUser.Email));
            ModelState.Remove(nameof(AppUser.Status));

            if (!ModelState.IsValid)
                return View(user);

            // Update editable fields
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Address = updatedUser.Address;
            user.City = updatedUser.City;
            user.State = updatedUser.State;
            user.ZipCode = updatedUser.ZipCode;
            user.PhoneNumber = updatedUser.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(user);
            }

            TempData["Message"] = "Profile updated successfully.";
            return RedirectToAction("Manage");
        }

        // ============================================================
        // CHANGE PASSWORD
        // ============================================================
        public IActionResult ChangePassword() => View();

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
                foreach (var e in result.Errors)
                    ModelState.AddModelError("", e.Description);

                return View(cpvm);
            }

            await _signInManager.RefreshSignInAsync(user);

            // EMAIL — Password Changed
            await _emailSender.SendEmailAsync(
                user.Email,
                "Team 24: Your Password Has Been Updated",
                EmailTemplate.Wrap($@"
                    <h2>Password Updated</h2>
                    <p>Hello {user.FirstName},</p>
                    <p>Your Bevo’s Books password has been successfully changed.</p>
                    <p>If this wasn’t you, please contact support immediately.</p>
                ")
            );

            TempData["Message"] = "Password changed successfully.";
            return RedirectToAction("Manage");
        }

        // ============================================================
        // ADD CARD (GET / POST)
        // ============================================================
        public IActionResult AddCard() => View();

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCard(CardViewModel cvm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(cvm);

            // limit 3 cards
            int cardCount = await _context.Cards.CountAsync(c => c.UserID == user.Id);
            if (cardCount >= 3)
            {
                ModelState.AddModelError("", "You can only register up to three credit cards on Bevo's Books.");
                return View(cvm);
            }

            Card card = new()
            {
                CardType = cvm.CardType,
                CardNumber = cvm.CardNumber,
                UserID = user.Id,
                CustomerName = $"{user.FirstName} {user.LastName}"
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return RedirectToAction("Checkout", "Orders");
        }

        // ============================================================
        // MY CARDS
        // ============================================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MyCards()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var cards = await _context.Cards
                .Where(c => c.UserID == user.Id)
                .OrderBy(c => c.CardType)
                .ToListAsync();

            return View(cards);
        }

        // ============================================================
        // REMOVE CARD
        // ============================================================
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCard(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var card = await _context.Cards.FirstOrDefaultAsync(c => c.CardID == id && c.UserID == user.Id);
            if (card == null)
            {
                TempData["AlertClass"] = "danger";
                TempData["Message"] = "Card not found.";
                return RedirectToAction(nameof(MyCards));
            }

            bool isUsed = await _context.OrderDetails.AnyAsync(od => od.CardID == id);
            if (isUsed)
            {
                TempData["AlertClass"] = "warning";
                TempData["Message"] = "This card was used in an order and cannot be removed.";
                return RedirectToAction(nameof(MyCards));
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            TempData["AlertClass"] = "success";
            TempData["Message"] = "Card removed.";
            return RedirectToAction(nameof(MyCards));
        }
    }
}