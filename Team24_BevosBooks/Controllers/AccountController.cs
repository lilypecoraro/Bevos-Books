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

            // Find the user first
            var user = await _userManager.FindByEmailAsync(lvm.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(lvm);
            }

            // ✅ Custom check: disabled accounts
            if (user.Status == AppUser.UserStatus.Disabled)
            {
                ModelState.AddModelError("", "Your account has been disabled. Please contact support.");
                return View(lvm);
            }

            // ✅ NEW: fired employees
            if (user.Status == AppUser.UserStatus.Fired)
            {
                ModelState.AddModelError("", "Your employment has been terminated. Please contact an administrator about rehire options.");
                return View(lvm);

            }
                // Attempt sign-in only if account is active
                var result = await _signInManager.PasswordSignInAsync(
                lvm.Email, lvm.Password, lvm.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
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
            TempData["LogoutMessage"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        // ============================================================
        // MANAGE ACCOUNT (GET)
        // ============================================================
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                TempData["AlertClass"] = "danger";
                TempData["Message"] = "You must be logged in to manage your account.";
                return RedirectToAction("Login", "Account");
            }

            // Do NOT clear TempData here — let messages from POST persist
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

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["AlertClass"] = "danger";
                TempData["Message"] = "You must be logged in to manage your account.";
                return RedirectToAction("Login", "Account");
            }

            // Remove Identity-only fields so validation passes
            ModelState.Remove(nameof(AppUser.Id));
            ModelState.Remove(nameof(AppUser.UserName));
            ModelState.Remove(nameof(AppUser.Email));
            ModelState.Remove(nameof(AppUser.Status));

            // Suppress validation for FirstName/LastName when the account is Employee
            if (user.Status == AppUser.UserStatus.Employee)
            {
                ModelState.Remove(nameof(AppUser.FirstName));
                ModelState.Remove(nameof(AppUser.LastName));

                // Also clear any existing model errors for those keys if they were added by data annotations
                if (ModelState.TryGetValue(nameof(AppUser.FirstName), out var firstState))
                    firstState.Errors.Clear();
                if (ModelState.TryGetValue(nameof(AppUser.LastName), out var lastState))
                    lastState.Errors.Clear();
            }

            if (!ModelState.IsValid)
            {
                TempData["AlertClass"] = "danger";
                TempData["Message"] = "There were validation errors. Please correct them and try again.";
                return View(user);
            }

            // Update editable fields
            if (user.Status != AppUser.UserStatus.Employee)
            {
                // Allow name changes for non-employees
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
            }
            // Always allow contact/address updates
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

                TempData["AlertClass"] = "danger";
                TempData["Message"] = "Profile update failed. Please review the errors.";
                return View(user);
            }

            TempData["AlertClass"] = "success";
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

            // ⭐ Assign next CardID
            int nextId = _context.Cards.Any()
                ? _context.Cards.Max(c => c.CardID) + 1
                : 1001;

            Card card = new()
            {
                CardID = nextId,  // ⭐ FIX
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
        [Authorize] // have to let the request hit the action -- employees/admin don't have their own CCs
        public async Task<IActionResult> MyCards()
        {
            if (!User.IsInRole("Customer"))
                return View("AccessDeniedEmployee");
            
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCard(int id)
        {
            if (!User.IsInRole("Customer"))
                return View("AccessDeniedEmployee");
            
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