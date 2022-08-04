namespace Waterful;

public static class Helpers
{
    public static void TryAsyncAndIgnore(Func<Task> action)
    {
        try
        {
            action();
        }

        catch { }
    }
}
