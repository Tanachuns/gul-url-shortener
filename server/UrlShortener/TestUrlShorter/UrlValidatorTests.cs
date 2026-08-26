using Xunit;
using FluentAssertions;
using UrlShortener.Services;

public class UrlValidatorTests
{
    private const string CurrentDomain = "localhost";

    [Theory]
    [InlineData("https://google.com")]
    [InlineData("http://example.org/path/to/resource?query=123")]
    [InlineData("https://subdomain.domain.co.uk/page#anchor")]
    public void IsValidUrl_WithValidHttpAndHttpsUrls_ReturnsTrue(string validUrl)
    {
        // Act
        bool result = UrlService.IsValidUrl(validUrl, CurrentDomain);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsValidUrl_WithNullOrWhitespace_ReturnsFalse(string invalidUrl)
    {
        // Act
        bool result = UrlService.IsValidUrl(invalidUrl, CurrentDomain);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("ftp://files.example.com")]
    [InlineData("javascript:alert(1)")]
    [InlineData("file:///C:/passwords.txt")]
    [InlineData("mailto:user@example.com")]
    public void IsValidUrl_WithNonHttpOrHttpsScheme_ReturnsFalse(string unsupportedSchemeUrl)
    {
        // Act
        bool result = UrlService.IsValidUrl(unsupportedSchemeUrl, CurrentDomain);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("not-a-url")]
    [InlineData("http://")]
    [InlineData("https://   .com")]
    [InlineData("://missing-scheme.com")]
    public void IsValidUrl_WithMalformedUrl_ReturnsFalse(string malformedUrl)
    {
        // Act
        bool result = UrlService.IsValidUrl(malformedUrl, CurrentDomain);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("https://localhost/xyz123")]
    [InlineData("http://locAlhost/custom-alias")]
    [InlineData("https://locAlhost/api/links")]
    public void IsValidUrl_WithSelfReferentialDomain_ReturnsFalse(string selfDomainUrl)
    {
        // Act
        bool result = UrlService.IsValidUrl(selfDomainUrl, CurrentDomain);
        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("http://localhost/test")]
    [InlineData("http://localhost:5001")]
    [InlineData("http://127.0.0.1/dashboard")]
    public void IsValidUrl_WithLoopbackOrLocalhost_ReturnsFalse(string loopbackUrl)
    {
        // Act
        bool result = UrlService.IsValidUrl(loopbackUrl, CurrentDomain);

        // Assert
        result.Should().BeFalse();
    }
}