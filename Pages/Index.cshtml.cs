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
    public IEnumerable<Water> Logs { get; set; } = default!;

    [BindProperty]
    public string? Option { get; set; }

    public async Task OnGetAsync(string? option)
    {
        Option = option != null ? option.AddSpacing() : "All";
        if (_context.Water is not null)
        {
            if (option is null)
                Logs = await _context.Water.ToListAsync();

            else
            {
                var parsedOption = (WaterType) Enum.Parse(typeof(WaterType), option);
                var logs = await _context.Water.ToListAsync();
                Logs = logs.Where(log => log.Type == parsedOption);
            }
        }
    }
}
