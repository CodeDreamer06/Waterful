using Microsoft.EntityFrameworkCore;
using Waterful.Models;

namespace Waterful.Data;

class DbService
{
    private readonly WaterContext _context;

    public DbService(WaterContext context) => _context = context;

    public async Task<List<Water>> GetLogs() => await _context.Water.ToListAsync();

    public async Task<Water> GetLogById(int id) => 
        await _context.Water.FirstAsync(m => m.Id == id);

    public bool WaterExists(int id) => 
        (_context.Water?.Any(e => e.Id == id)).GetValueOrDefault();

    public async Task<List<Water>> GetLogsByOption(string option)
    {
        if (_context.Water is not null)
        {
            if (option is null || option == "All") return await GetLogs();

            else
            {
                Enum.TryParse(option, out WaterType parsedOption);
                return GetLogs().Result.Where(log => log.Type == parsedOption).ToList();
            }
        }

        return new();
    }

    public void DeleteSelectedLogs(Dictionary<string, bool> selectedLogs)
    {
        foreach (var checkbox in selectedLogs)
            if (checkbox.Value)
                _context.Water.Remove(new Water() { Id = int.Parse(checkbox.Key) });
    }

    public async Task DeleteLog(Water log)
    {
        _context.Water.Remove(log);
        await _context.SaveChangesAsync();
    }

    public async Task<int> AddLogs(Func<IEnumerable<Water>> getLogs)
    {
        var totalQuantity = 0;

        foreach (var log in getLogs())
        {
            _context.Water.Add(log);
            totalQuantity += log.Quantity;
        }

        await _context.SaveChangesAsync();
        return totalQuantity;
    }

    public void TryToSaveChanges() => 
        Helpers.TryAsyncAndIgnore(async () => await _context.SaveChangesAsync());

    public async Task<bool> MarkAsUpdatedAndSaveLog(Water log)
    {
        _context.Attach(log).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }

        catch (DbUpdateConcurrencyException)
        {
            if (!WaterExists(log.Id)) return false;
            throw;
        }
    }
}
