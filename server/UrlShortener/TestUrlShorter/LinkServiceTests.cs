using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Databases;
using UrlShortener.Models;
using UrlShortener.Models.Http;
using UrlShortener.Services;
using Xunit;
using FluentAssertions;


public class LinkServiceTests
{
    private LinkContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<LinkContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // แยก DB ใหม่ทุกครั้งที่รันแต่ละ test
            .Options;

        return new LinkContext(options);
    }

    #region CreateShortUrlAsync Tests

    [Fact]
    public async Task CreateShortUrlAsync_WithoutCustomAlias_ShouldSaveLinkAndGenerateCode()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        var request = new CreateShortlinkRequestModel
        {
            Url = "https://example.com",
            iosUrl = "https://example.com/ios",
            androidUrl = "https://example.com/android",
            CustomAlias = null
        };

        // Act
        string result = await service.CreateShortUrlAsync(request);

        // Assert
        result.Should().NotBeNullOrEmpty();
        
        var savedLink = await context.Links.FirstOrDefaultAsync(l => l.Code == result);
        savedLink.Should().NotBeNull();
        savedLink!.LongUrl.Should().Be("https://example.com");
        savedLink.LongAplUrl.Should().Be("https://example.com/ios");
        savedLink.LongAndUrl.Should().Be("https://example.com/android");
        savedLink.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task CreateShortUrlAsync_WithCustomAlias_ShouldUseCustomAliasAsCode()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        var request = new CreateShortlinkRequestModel
        {
            Url = "https://example.com",
            CustomAlias = "my-custom-link"
        };

        // Act
        string result = await service.CreateShortUrlAsync(request);

        // Assert
        result.Should().Be("my-custom-link");
        
        var savedLink = await context.Links.FirstOrDefaultAsync(l => l.Code == "my-custom-link");
        savedLink.Should().NotBeNull();
    }

    #endregion

    #region Find & FindAll Tests

    [Fact]
    public async Task Find_WhenIsActiveIsNull_ShouldReturnMatchingLink()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        context.Links.Add(new LinkModel { Code = "test1", LongUrl = "https://a.com", IsActive = false });
        await context.SaveChangesAsync();

        // Act
        var result = service.Find("test1", isActive: null);

        // Assert
        result.Should().NotBeNull();
        result!.Code.Should().Be("test1");
    }

    [Theory]
    [InlineData(true, true)]   // หา link ที่ active = true เจอ
    [InlineData(false, false)] // หา link ที่ active = false เจอ
    public async Task Find_WithIsActiveFilter_ShouldReturnCorrectLink(bool linkStatus, bool searchStatus)
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        context.Links.Add(new LinkModel { Code = "test-code", LongUrl = "https://a.com", IsActive = linkStatus });
        await context.SaveChangesAsync();

        // Act
        var result = service.Find("test-code", isActive: searchStatus);

        // Assert
        result.Should().NotBeNull();
        result!.IsActive.Should().Be(searchStatus);
    }

    [Fact]
    public async Task Find_WhenNotFound_ShouldReturnNull()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);

        // Act
        var result = service.Find("not-exist-code", isActive: true);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task FindAll_ShouldReturnAllLinks()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        context.Links.AddRange(
            new LinkModel { Code = "code1", LongUrl = "https://a.com" },
            new LinkModel { Code = "code2", LongUrl = "https://b.com" }
        );
        await context.SaveChangesAsync();

        // Act
        var result = service.FindAll();

        // Assert
        result.Should().HaveCount(2);
    }

    #endregion

    #region AddCount Tests

    [Fact]
    public async Task AddCount_WhenLinkExists_ShouldIncrementVisitedAndSetLastAccessed()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        var link = new LinkModel { Code = "code1", Visited = 0, LongUrl = "https://a.com" };
        context.Links.Add(link);
        await context.SaveChangesAsync();

        // Act
        await service.AddCount(link);

        // Assert
        var updatedLink = await context.Links.FindAsync(link.Id);
        updatedLink!.Visited.Should().Be(1);
        updatedLink.LastAccessed.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task AddCount_WhenLinkNotFound_ShouldThrowException()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        var nonExistentLink = new LinkModel { Id = 999, Code = "not-exist" };

        // Act
        Func<Task> act = async () => await service.AddCount(nonExistentLink);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Link not found");
    }

    #endregion

    #region SetActive Tests

    [Fact]
    public async Task SetActive_WhenStatusChanges_ShouldUpdateIsActive()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        var link = new LinkModel { Code = "code1", IsActive = true, LongUrl = "https://a.com" };
        context.Links.Add(link);
        await context.SaveChangesAsync();

        // Act
        await service.SetActive(link, isActive: false);

        // Assert
        var updatedLink = await context.Links.FindAsync(link.Id);
        updatedLink!.IsActive.Should().BeFalse();
    }

    [Fact]
    public async Task SetActive_WhenStatusIsSameOrNotFound_ShouldThrowException()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new LinkService(context);
        var link = new LinkModel { Code = "code1", IsActive = true, LongUrl = "https://a.com" };
        context.Links.Add(link);
        await context.SaveChangesAsync();

        // Act: พยายามปรับ active = true ทั้งๆ ที่เป็น true อยู่แล้ว (เข้าเงื่อนไข l.IsActive != isActive)
        Func<Task> act = async () => await service.SetActive(link, isActive: true);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Link not found");
    }

    #endregion
}