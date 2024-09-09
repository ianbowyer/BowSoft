using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using static System.Net.HttpStatusCode;
using System.Net;
using BowSoft.TestExtensions.TestingApplication.Models;

namespace BowSoft.TestExtensions.Tests;

public class ShouldBeJsonObjectWithActionShould(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

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
        await response.ShouldBeJson<Customer>(customer =>
            customer
                .Should()
                .BeEquivalentTo(expectedCustomer)
            );

        var actual = await response.ShouldBeJson<Customer>();
    }
}