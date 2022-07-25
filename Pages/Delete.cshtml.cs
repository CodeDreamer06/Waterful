using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Waterful.Models;

namespace Waterful.Pages;

public class DeleteModel : PageModel
{
    private readonly Data.WaterContext _context;

    public DeleteModel(Data.WaterContext context) => _context = context;

    [BindProperty]
    public Water Water { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null || _context.Water is null) 
            return NotFound();

        var water = await _context.Water.FirstOrDefaultAsync(m => m.Id == id);

        if (water is null) return NotFound();
        
        Water = water;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null || _context.Water is null)
            return NotFound();

        var water = await _context.Water.FindAsync(id);

        if (water is not null)
        {
            Water = water;
            _context.Water.Remove(Water);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
