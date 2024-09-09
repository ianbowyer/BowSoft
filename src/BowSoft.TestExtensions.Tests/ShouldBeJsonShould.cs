using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using BowSoft.TestExtensions.TestingApplication.Models;

namespace BowSoft.TestExtensions.Tests;

public class ShouldBeJsonShould(WebApplicationFactory<Program> factory)
: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task ReturnsTrueIfJsonIsValid()
    {
        // Arrange
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/tester/ReturnsJsonObject");

        // Act
        var actual = await response.ShouldBeJson();

        // Assert
        actual.Should().BeTrue();
    }

    [Theory]
    [InlineData("thequickbrownfoxjumpsoverthelazydog")]
    [InlineData("{ name =\"Bobby\" } ")]
    [InlineData("{ name = Bobby } ")]
    public async Task ReturnsFalseIfJsonIsNotValid(string exampleString)
    {
        // Arrange
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"api/tester/ReturnsString/{exampleString}");

        // Act
        var actual = await response.ShouldBeJson();

        // Assert
        actual.Should().BeFalse();
    }

    [Theory]
    [InlineData("{\"name\":\"Bobby\"}")]
    public async Task ReturnsFalseIfJsonIsValid(string exampleString)
    {
        // Arrange
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"api/tester/ReturnsString/{exampleString}");

        // Act
        var actual = await response.ShouldBeJson();

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public async Task ReturnExpectedObject()
    {
        // Arrange
        var client = _factory.CreateClient();
        var expectedCustomer = new Customer()
        {
            Id = 42,
            Name = "Ian Bowyer",
            City = "Leeds"
        };
        var response = await client.GetAsync("api/tester/ReturnsJsonObject");

        // Act / Assert
        var actual = await response.ShouldBeJson<Customer>();
        actual.Should().BeEquivalentTo(expectedCustomer);
    }
}