using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Waterful.Models;

namespace Waterful.Pages;

public class IndexModel : PageModel
{
    private readonly Data.WaterContext _context;

    public IndexModel(Data.WaterContext context) => _context = context;

    [BindProperty]
    public IList<Water> Water { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Water is not null) 
            Water = await _context.Water.ToListAsync();
    }
}
