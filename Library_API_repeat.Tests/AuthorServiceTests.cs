using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services;
using Microsoft.EntityFrameworkCore;

using Library_API_repeat.Api.DTOs.Authors;

namespace Library_API_repeat.Tests;

public class AuthorServiceTests
{
    private LibraryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new LibraryDbContext(options);
    }
    [Fact]
    public async Task GetAllAsync_ReturnsAuthors()
    {
        // Arrange
        using var context = CreateContext();

        context.Authors.AddRange(
            new Author
            {
                name = "George Orwell",
                Country = "United Kingdom"
            },
            new Author
            {
                name = "J.R.R. Tolkien",
                Country = "United Kingdom"
            }
        );

        await context.SaveChangesAsync();

        var service = new AuthorService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }
    [Fact]
    public async Task GetByIdAsync_ReturnsAuthor_WhenAuthorExists()
    {
        // Arrange
        using var context = CreateContext();

        var author = new Author
        {
            name = "George Orwell",
            Country = "United Kingdom"
        };

        context.Authors.Add(author);
        await context.SaveChangesAsync();

        var service = new AuthorService(context);

        // Act
        var result = await service.GetByIdAsync(author.id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("George Orwell", result.Name);
        Assert.Equal("United Kingdom", result.Country);
    }
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenAuthorDoesNotExist()
    {
        // Arrange
        using var context = CreateContext();

        var service = new AuthorService(context);

        // Act
        var result = await service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task CreateAsync_AddsAuthor()
    {
        // Arrange
        using var context = CreateContext();

        var service = new AuthorService(context);

        var dto = new CreateAuthorDTO
        {
            Name = "J.R.R. Tolkien",
            Country = "United Kingdom"
        };

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("J.R.R. Tolkien", result.Name);

        Assert.Equal(1, await context.Authors.CountAsync());
    }
    [Fact]
    public async Task UpdateAsync_UpdatesExistingAuthor()
    {
        // Arrange
        using var context = CreateContext();

        var author = new Author
        {
            name = "George Orwell",
            Country = "United Kingdom"
        };

        context.Authors.Add(author);
        await context.SaveChangesAsync();

        var service = new AuthorService(context);

        var dto = new UpdateAuthorDTO
        {
            Name = "George Orwell",
            Country = "England"
        };

        // Act
        var result = await service.UpdateAsync(author.id, dto);

        // Assert
        Assert.True(result);

        var updatedAuthor = await context.Authors.FindAsync(author.id);

        Assert.NotNull(updatedAuthor);
        Assert.Equal("England", updatedAuthor.Country);
    }
    [Fact]
    public async Task DeleteAsync_RemovesAuthor()
    {
        // Arrange
        using var context = CreateContext();

        var author = new Author
        {
            name = "George Orwell",
            Country = "United Kingdom"
        };

        context.Authors.Add(author);
        await context.SaveChangesAsync();

        var service = new AuthorService(context);

        // Act
        var result = await service.DeleteAsync(author.id);

        // Assert
        Assert.True(result);

        Assert.Equal(0, await context.Authors.CountAsync());
    }
}