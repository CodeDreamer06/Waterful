using Microsoft.AspNetCore.Mvc.RazorPages;
using Waterful.Models;

namespace Waterful.Pages;

public class IndexModel : PageModel
{
    private readonly DbService db;

    public List<WaterModel> Records { get; set; } = new();

    public IndexModel(IConfiguration config) => db = new(config);

    public void OnGet()
    {
        Records = db.GetLogs();
        ViewData["Total"] = Records.AsEnumerable().Sum(log => log.Quantity);
    }
}
