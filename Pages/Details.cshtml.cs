using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Waterful.Models;

namespace Waterful.Pages;

public class DetailsModel : PageModel
{
    private readonly Data.WaterContext _context;

    public DetailsModel(Data.WaterContext context) => _context = context;

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
}
