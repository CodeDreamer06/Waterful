using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Waterful.Models;

public enum WaterType
{
    [Display(Name = "Mini Glass")]
    MiniGlass,

    [Display(Name = "Glass")]
    Glass,

    [Display(Name = "Bottle")]
    Bottle
}

public class Water
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required, Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
    public int Quantity { get; set; }

    [Required]
    public WaterType Type { get; set; }

    public Water()
    {
    }

    public Water(Water oldObject)
    {
        Date = oldObject.Date;
        Quantity = oldObject.Quantity;
        Type = oldObject.Type;
    }
}
