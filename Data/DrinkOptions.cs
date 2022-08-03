using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Waterful.Models;

namespace Waterful.Data;

public class DrinkOptions
{
    public ViewDataDictionary ViewData { get; set; }

    public ViewDataDictionary MiniGlass { get; set; }

    public ViewDataDictionary Glass { get; set; }

    public ViewDataDictionary Bottle { get; set; }

    public DrinkOptions(ViewDataDictionary viewData)
    {
        ViewData = viewData;

        MiniGlass = new(ViewData)
        {
            {
                "option",
                new DrinkOption()
                {
                    HiddenIdName = "mini-glass",
                    Title = "100ml"
                }
            }
        };

        Glass = new(ViewData)
        {
            {
                "option",
                new DrinkOption()
                {
                    HiddenIdName = "glass",
                    Title = "250ml"
                }
            }
        };

        Bottle = new(ViewData)
        {
            {
                "option",
                new DrinkOption()
                {
                    HiddenIdName = "bottle",
                    Title = "450ml"
                }
            }
        };
    }
}
