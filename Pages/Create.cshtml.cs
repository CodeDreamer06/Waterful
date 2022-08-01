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

        [BindProperty]
        public int[] Quantities { get; set; } = new int[3];

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Water is null || Water is null)
                return Page();

            foreach (var record in SetRecords())
            {
                _context.Water.Add(record);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private IEnumerable<Water> SetRecords()
        {
            var waterTypes = Enum.GetValues(typeof(WaterType)).Cast<WaterType>().ToList();
            Water.Date = DateTime.Now;

            for (int i = 0; i < Quantities.Length; i++)
            {
                if(Quantities[i] != 0)
                    yield return new Water(Water) 
                    { 
                        Quantity = Quantities[i], Type = waterTypes[i] 
                    };
            }
        }
    }
}
