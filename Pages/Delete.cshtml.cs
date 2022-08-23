using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waterful.Data;
using Waterful.Models;

namespace Waterful.Pages;

public class DeleteModel : PageModel
{
    private readonly DbService _db;

    public DeleteModel(WaterContext context) => _db = new(context);

    [BindProperty]
    public Water Water { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var water = await _db.GetLogById(id.Value);
        if (water is null) return NotFound();
        
        Water = water;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null) return NotFound();

        var water = await _db.GetLogById(id.Value);

        if (water is not null)
        {
            Water = water;
            await _db.DeleteLog(Water);

            NotificationBuilder.RemovedSuccessfully(water.Quantity);
        }

        return RedirectToPage("./Index");
    }
}
