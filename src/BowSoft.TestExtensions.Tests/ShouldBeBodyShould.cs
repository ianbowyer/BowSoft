using Microsoft.AspNetCore.Mvc.Testing;

namespace BowSoft.TestExtensions.Tests;

public class ShouldBeBodyShould(WebApplicationFactory<Program> factory)
: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task ReturnsTrueIfJsonIsValid()
    {
        // Arrange
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/tester/ReturnsString/ABC");

        // Act
        var actual = await response.ShouldBeBody();

        // Assert
    }
}