﻿namespace Waterful.Models;

public static class Notification
{
    public static string Title { get; set; } = default!;

    public static string Message { get; set; } = default!;

    public static string Type { get; set; } = default!;

    public static bool IsNotEmpty() => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Message);

    public static void Clear()
    {
        Title = string.Empty;
        Message = string.Empty;
    }

    public static void SetNotification(string Title, string Message, string Type)
    {
        Notification.Title = Title;
        Notification.Message = Message;
        Notification.Type = Type;
    }
}
