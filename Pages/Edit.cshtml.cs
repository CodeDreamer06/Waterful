using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Waterful.Models;
using Waterful.Data;

namespace Waterful.Pages;

public class EditModel : PageModel
{
    private readonly DbService _db;

    public EditModel(WaterContext context) => _db = new(context);

    [BindProperty]
    public Water Water { get; set; } = default!;

    [BindProperty]
    public int[] Quantities { get; set; } = new int[3];

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var water = await _db.GetLogById(id.Value);
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

        var logExists = await _db.MarkAsUpdatedAndSaveLog(Water);
        if (!logExists) return NotFound();

        NotificationBuilder.UpdatedSuccessfully(Water);
        return RedirectToPage("./Index");
    }    

    private async Task<Water> GetLog()
    {
        var water = await _db.GetLogById(Water.Id);
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
}
