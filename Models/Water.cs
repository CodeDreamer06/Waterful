using System.ComponentModel.DataAnnotations;

namespace Waterful.Models;

public enum WaterType
{
    MiniGlass,
    Glass,
    Bottle
}

public class Water
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required, Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
    public int Quantity { get; set; }

    [Required]
    public WaterType Type { get; set; }
}
