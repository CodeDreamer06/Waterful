using Waterful.Models;

namespace Waterful.Data;

public static class NotificationBuilder
{
    public static void AddedSuccessfully(int quantity)
    {
        var quantitySuffix = GetQuantitySuffix(quantity);

        var title = $"Successfully added your {quantitySuffix}!";
        var message = $"Added {quantity} {quantitySuffix}";

        Notification.SetNotification(title, message, "success");
    }

    public static void UpdatedSuccessfully(Water log)
    {
        var quantity = log.GetTotalQuantityByRecord().ConvertToReadableUnits();

        var title = "Successfully changed the quantity";
        var message = $"Changed the quantity of your {log.Type} to {quantity}";

        Notification.SetNotification(title, message, "success");
    }

    public static void UpdateFailed()
    {
        var title  = "Failed to edit log";
        var message = "You can only edit the count of an existing log type.";

        Notification.SetNotification(title, message, "error");
    }

    public static void RemovedSuccessfully(int quantity)
    {
        var quantitySuffix = GetQuantitySuffix(quantity);

        var title = $"Successfully removed your {quantitySuffix}";
        var message = $"Removed {quantity} {quantitySuffix}";

        Notification.SetNotification(title, message, "success");
    }

    private static string GetQuantitySuffix(int quantity) => quantity == 1 ? "log" : "logs";
}
