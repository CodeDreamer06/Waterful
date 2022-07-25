using Microsoft.EntityFrameworkCore;

namespace Waterful.Data;

public class WaterContext : DbContext
{
    public WaterContext(DbContextOptions<WaterContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Water> Water { get; set; } = default!;
}
