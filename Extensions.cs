using System.Globalization;
using System.Text.RegularExpressions;
using Waterful.Models;

namespace Waterful;

public static class Extensions
{
    public static string AddSpacing(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => Regex.Replace(input, "([A-Z])", " $1").Trim().ToLower().FirstCharToUpper()
        };

    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

    public static string GetTodayQuantity(this IEnumerable<Water> records)
    {
        double totalQuantity = records.Aggregate(0d, (total, log) => total + (log.Date.Date == DateTime.Now.Date ? GetTotalQuantityByRecord(log) : 0));
        return ConvertToReadableUnits(totalQuantity);
    }

    public static double GetTotalQuantityByRecord(this Water log) => GetQuantityFromWaterType(log.Type) * log.Quantity;

    public static string ConvertToReadableUnits(this double totalQuantity) =>
        totalQuantity >= 1000 ? totalQuantity / 1000 + "L" : totalQuantity + "ml";

    public static int GetQuantityFromWaterType(this WaterType waterType) => waterType switch
    {
        WaterType.MiniGlass => 100,
        WaterType.Glass => 250,
        WaterType.Bottle => 450,
        _ => throw new InvalidOperationException()
    };

    public static string FormatLogDate(this DateTime date)
    {
        var time = date.ToString("t", CultureInfo.CreateSpecificCulture("en-US"));
        if (date.Date == DateTime.Now.Date)
            return $"Today, {time}";

        else if (date.Date == DateTime.Now.Date.AddDays(-1))
            return $"Yesterday, {time}";

        return date.ToString("f", CultureInfo.CreateSpecificCulture("en-US"));
    }
}
