using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Waterful.Models;
using Waterful.Data;

namespace Waterful.Pages
{
    public class EditModel : PageModel
    {
        private readonly WaterContext _context;

        public EditModel(WaterContext context) => _context = context;

        [BindProperty]
        public Water Water { get; set; } = default!;

        [BindProperty]
        public int[] Quantities { get; set; } = new int[3];

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null || _context.Water is null)
                return NotFound();

            var water = await _context.Water.FirstOrDefaultAsync(m => m.Id == id);
            if (water is null) return NotFound();

            Water = water;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            Water = await GetLog();

            if (Water.Id is -1)
            {
                NotificationBuilder.UpdateFailed();
                return RedirectToPage("./Index");
            }

            var logExists = await SaveChanges();
            if (!logExists) return NotFound();

            NotificationBuilder.UpdatedSuccessfully(Water);
            return RedirectToPage("./Index");
        }

        private async Task<bool> SaveChanges()
        {
            _context.Attach(Water).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!WaterExists(Water.Id)) return false;
                throw;
            }
        }

        private async Task<Water> GetLog()
        {
            var water = await _context.Water.FirstOrDefaultAsync(m => m.Id == Water.Id);
            var waterTypes = Enum.GetValues(typeof(WaterType)).Cast<WaterType>().ToList();

            for (int i = 0; i < Quantities.Length; i++)
            {
                if (Quantities[i] != 0)
                {
                    if (waterTypes.IndexOf(water!.Type) != i) water!.Id = -1;
                    else water!.Quantity = Quantities[i];
                }
            }

            return water!;
        }

        private bool WaterExists(int id) => 
            (_context.Water?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
