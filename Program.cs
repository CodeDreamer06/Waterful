using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Waterful.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<WaterContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WaterContext")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
