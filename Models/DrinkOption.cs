namespace Waterful.Models;

public record DrinkOption
{
    public string HiddenIdName { get; set; } = default!;

    public string Title { get; set; } = default!;

    public int DefaultQuantity { get; set; } = 0;

    //public string ImageSource { get; set; } = default!;
}
