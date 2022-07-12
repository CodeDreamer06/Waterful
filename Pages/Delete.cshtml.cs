using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waterful.Models;

namespace Waterful.Pages;

public class DeleteModel : PageModel
{
    private readonly DbService db;

    [BindProperty]
    public WaterModel Water { get; set; } = new();

    public DeleteModel(IConfiguration config) => db = new(config);

    public IActionResult OnGet(int id)
    {
        Water = db.GetLogs($"SELECT * FROM logs WHERE Id = {id}").First();
        return Page();
    }

    public IActionResult OnPost(int id)
    {
        db.Execute($"DELETE FROM logs WHERE Id = {id}");
        return RedirectToPage("./Index");
    }
}
