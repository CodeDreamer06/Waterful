using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Waterful.Models;

namespace Waterful.Pages
{
    public class EditModel : PageModel
    {
        private readonly Data.WaterContext _context;

        public EditModel(Data.WaterContext context) => _context = context;

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

            _context.Attach(Water).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!WaterExists(Water.Id)) return NotFound();
                throw;
            }

            return RedirectToPage("./Index");
        }

        private async Task<Water> GetLog()
        {
            var water = await _context.Water.FirstOrDefaultAsync(m => m.Id == Water.Id);

            foreach (var quantity in Quantities)
                if (quantity != 0) water!.Quantity = quantity;

            return water!;
        }

        private bool WaterExists(int id) => 
            (_context.Water?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
