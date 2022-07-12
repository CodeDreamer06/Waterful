using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waterful.Models;

namespace Waterful.Pages;

public class UpdateModel : PageModel
{
    private readonly DbService db;

    [BindProperty]
    public WaterModel Water { get; set; } = new();

    public UpdateModel(IConfiguration config) => db = new(config);

    public IActionResult OnGet(int id)
    {
        Water = db.GetLogs($"SELECT * FROM logs WHERE Id = {id}").First();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();
        db.Execute($"UPDATE logs SET Date = '{Water.Date}', Quantity = '{Water.Quantity}' WHERE Id = {Water.Id}");
        return RedirectToPage("./Index");
    }
}
