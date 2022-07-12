using System.ComponentModel.DataAnnotations;

namespace Waterful.Models;

public class WaterModel
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
    public int Quantity { get; set; }
}
