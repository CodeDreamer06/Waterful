using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waterful.Data;
using Waterful.Models;

namespace Waterful.Pages;

public class CreateModel : PageModel
{
    private readonly DbService _db;

    public CreateModel(WaterContext context) => _db = new(context);

    public IActionResult OnGet() => Page();

    [BindProperty]
    public Water Water { get; set; } = default!;

    [BindProperty]
    public int[] Quantities { get; set; } = new int[3];

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || Water is null)
            return Page();

        var totalQuantity = await _db.AddLogs(GetLogs);

        NotificationBuilder.AddedSuccessfully(totalQuantity);
        return RedirectToPage("./Index");
    }

    IEnumerable<Water> GetLogs()
    {
        var waterTypes = Enum.GetValues(typeof(WaterType)).Cast<WaterType>().ToList();
        Water.Date = DateTime.Now;

        foreach (var quantity in Quantities)
        {
            if (quantity != 0)
                yield return new Water(Water)
                {
                    Quantity = quantity,
                    Type = waterTypes.First()
                };

            waterTypes.RemoveAt(0);
        }
    }
}
