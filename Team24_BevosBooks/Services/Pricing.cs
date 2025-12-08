using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Services
{
    public static class Pricing
    {
        // Returns the effective customer price given any active item discount for the book
        public static async Task<decimal> GetEffectivePriceAsync(AppDbContext ctx, Book book)
        {
            // find enabled discount in window (if dates set)
            var now = DateTime.Now;
            var disc = await ctx.ItemDiscounts
                .Where(d => d.BookID == book.BookID && d.Status == "Enabled")
                .Where(d => (!d.StartDate.HasValue || d.StartDate <= now) &&
                            (!d.EndDate.HasValue || d.EndDate >= now))
                .OrderByDescending(d => d.DiscountPercent.HasValue ? d.DiscountPercent.Value : 0)
                .FirstOrDefaultAsync();

            if (disc == null) return book.Price;

            if (disc.DiscountPercent.HasValue && disc.DiscountPercent.Value > 0)
            {
                var pct = disc.DiscountPercent.Value / 100m;
                var discounted = book.Price * (1 - pct);
                return discounted < 0 ? 0 : discounted;
            }

            if (disc.DiscountAmount.HasValue && disc.DiscountAmount.Value > 0)
            {
                var discounted = book.Price - disc.DiscountAmount.Value;
                return discounted < 0 ? 0 : discounted;
            }

            return book.Price;
        }
    }
}