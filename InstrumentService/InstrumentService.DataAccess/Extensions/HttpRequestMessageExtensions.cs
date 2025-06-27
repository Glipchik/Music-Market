namespace InstrumentService.DataAccess.Extensions;

public static class HttpRequestMessageExtensions
{
    public static async Task<HttpRequestMessage> CloneHttpRequestMessage(this HttpRequestMessage req,
        CancellationToken cancellationToken)
    {
        var clone = new HttpRequestMessage(req.Method, req.RequestUri);

        if (req.Content != null)
        {
            var ms = new MemoryStream();
            await req.Content.CopyToAsync(ms, cancellationToken);
            ms.Position = 0;
            clone.Content = new StreamContent(ms);

            foreach (var header in req.Content.Headers)
            {
                clone.Content.Headers.Add(header.Key, header.Value);
            }
        }

        foreach (var header in req.Headers)
        {
            clone.Headers.Add(header.Key, header.Value);
        }

        clone.Version = req.Version;
        foreach (var option in req.Options)
        {
            clone.Options.Set(new HttpRequestOptionsKey<object?>(option.Key), option.Value);
        }

        return clone;
    }
}