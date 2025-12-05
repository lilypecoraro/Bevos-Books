using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShippingSettingsController : Controller
    {
        private readonly AppDbContext _context;

        public ShippingSettingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Edit shipping settings
        public async Task<IActionResult> Edit()
        {
            var setting = await _context.ShippingSettings.FirstOrDefaultAsync();

            // If no record exists, create one with defaults
            if (setting == null)
            {
                setting = new ShippingSetting
                {
                    FirstBookRate = 3.50m,
                    AdditionalBookRate = 1.50m
                };
                _context.ShippingSettings.Add(setting);
                await _context.SaveChangesAsync();
            }

            return View(setting);
        }

        // POST: Save shipping settings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ShippingSetting setting)
        {
            if (!ModelState.IsValid)
            {
                return View(setting);
            }

            try
            {
                _context.Update(setting);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Shipping rates updated successfully.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.ShippingSettings.AnyAsync(s => s.SettingID == setting.SettingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Edit));
        }
    }
}
