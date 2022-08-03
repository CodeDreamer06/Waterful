using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Waterful.Models;

namespace Waterful.Data;

public class DrinkOptions
{
    public ViewDataDictionary ViewData { get; set; }

    public ViewDataDictionary MiniGlass { get; set; }

    public ViewDataDictionary Glass { get; set; }

    public ViewDataDictionary Bottle { get; set; }

    public DrinkOptions(ViewDataDictionary viewData, Water? baseLog)
    {
        ViewData = viewData;

        MiniGlass = new(ViewData)
        {
            {
                "option",
                new DrinkOption()
                {
                    HiddenIdName = "mini-glass",
                    Title = "100ml",
                    DefaultQuantity = SetQuantity(baseLog, comparison: WaterType.MiniGlass)
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
                    Title = "250ml",
                    DefaultQuantity = SetQuantity(baseLog, comparison: WaterType.Glass)
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
                    Title = "450ml",
                    DefaultQuantity = SetQuantity(baseLog, comparison: WaterType.Bottle)
                }
            }
        };
    }

    private static int SetQuantity(Water? baseLog, WaterType comparison) => 
        baseLog != null ? baseLog.Type == comparison ? baseLog.Quantity : 0 : 0;
}
