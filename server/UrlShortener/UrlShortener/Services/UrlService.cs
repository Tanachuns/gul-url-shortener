namespace UrlShortener.Services;

public class UrlService
{
    public static bool IsValidUrl(string url
        //, string currentDomain
        )
    {
        // 1. Check null/empty
        if (string.IsNullOrWhiteSpace(url)) return false;

        // 2. Parse URI structure
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)) return false;

        // 3. Scheme check (HTTP/HTTPS only)
        if (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps) return false;

        // 4. Prevent self-referential shortening
       //if (uriResult.Host.Equals(currentDomain, StringComparison.OrdinalIgnoreCase)) return false;

        // 5. Block localhost / loopback (SSRF protection)
       // if (uriResult.IsLoopback || uriResult.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)) return false;

        return true;
    }
}