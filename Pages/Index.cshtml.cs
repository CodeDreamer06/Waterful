using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waterful.Models;
using Waterful.Data;

namespace Waterful.Pages;

public class IndexModel : PageModel
{
    private readonly DbService _db;

    public IndexModel(WaterContext context) => _db = new(context);

    [BindProperty]
    public List<Water> Logs { get; set; } = default!;

    [BindProperty]
    public string? Option { get; set; }

    [BindProperty]
    public string? TodayQuantity { get; set; }

    [BindProperty]
    public Dictionary<string, bool> IsChecked { get; set; } = default!;

    public async Task OnGetAsync(string? option)
    {
        Option = option != null ? option.AddSpacing() : "All";
        Logs = await _db.GetLogsByOption(Option);

        Logs.Reverse();
        TodayQuantity = Logs.GetTodayQuantity();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _db.DeleteSelectedLogs(IsChecked);
        _db.TryToSaveChanges();

        Logs = await _db.GetLogs();
        Logs.Reverse();
        TodayQuantity = Logs.GetTodayQuantity();

        return Page();
    }
}
