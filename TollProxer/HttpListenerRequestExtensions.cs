using System.Net;

public static class HttpListenerRequestExtensions
{
    public static string GetParam(this HttpListenerRequest request, string paramName)
    {
        var url = request.Url.ToString();
        var parameters = url.Substring(url.IndexOf('?') + 1);
        var pairs = parameters.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries).Select(x =>
        {
            var splits = x.Split(new[] { "=" }, StringSplitOptions.None);
            if (splits.Length != 2)
                throw new ArgumentException(string.Format("Wrong format of QueryString {0}", url));
            return new KeyValuePair<string, string>(splits[0], splits[1]);
        }).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.First().Value);
        return HttpHelpers.UrlUnescape(pairs[paramName]);
    }

    public static byte[] Body(this HttpListenerRequest request)
    {
        return request.InputStream.ReadBytes();
    }
}