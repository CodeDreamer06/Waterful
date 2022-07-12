using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waterful.Models;

namespace Waterful.Pages
{
    public class CreateModel : PageModel
    {
        private readonly DbService db;

        [BindProperty]
        public WaterModel Water { get; set; } = new();

        public CreateModel(IConfiguration config) => db = new(config);

        public IActionResult OnGet() => Page();

        public IActionResult OnPost()
		{
            if (!ModelState.IsValid) return Page();
            db.Execute(@$"INSERT INTO logs(DateTime, Quantity) VALUES('{Water!.Date}', '{Water.Quantity}')");

            return RedirectToPage("./Index");
        }
    }
}
