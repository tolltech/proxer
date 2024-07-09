public static class HttpHelpers
{
    public static string UrlEscape(string source)
    {
        return Uri.EscapeDataString(source ?? "");
    }

    public static string UrlUnescape(string source)
    {
        return Uri.UnescapeDataString(source ?? "");
    }
}