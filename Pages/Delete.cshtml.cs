using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Waterful.Data;
using Waterful.Models;

namespace Waterful.Pages;

public class DeleteModel : PageModel
{
    private readonly WaterContext _context;

    public DeleteModel(WaterContext context) => _context = context;

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

            NotificationBuilder.RemovedSuccessfully(water.Quantity);
        }

        return RedirectToPage("./Index");
    }
}
