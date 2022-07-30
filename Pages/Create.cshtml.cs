using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waterful.Models;

namespace Waterful.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Data.WaterContext _context;

        public CreateModel(Data.WaterContext context) => _context = context;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Water Water { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Water is null || Water is null)
                return Page();

            Water.Date = DateTime.Now;
            _context.Water.Add(Water);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
