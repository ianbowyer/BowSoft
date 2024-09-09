# BowSoft
BowSoft is a collection utilities for developing with .Net code.

# Introduction
I am beginning with some extension methods to use when testing. 
These target the HttpResponseMessage object and initially getting the body, confirming that it is JSON 
and checking if is the expected json from a test.

This is particularly useful for when doing integration tests calling an API. It will throw an Fluent Assertion 
if the object does not match what is expected.


# Getting Started
## BowSoft.TestExtensions
When using the library is Test Projects that return an HttpResponseMessage using the following methods:
- ShouldBeJson()
- ShouldBeJson\<T\>()
- ShouldBeJson\<T\>(Action)
- ShouldBeContentType(string)
- ShouldBeBody()

# Code Snippets

ShouldBeJson<T> is an extension method from HttpResponseMessage which goes and gets the content 
and deserialises in to a type of T. 


    // Fluent Assertions
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

        // Act
        var response = await client.GetAsync("api/tester/ReturnsJsonObject");

        // Assert
        var actual = await response.ShouldBeJson<Customer>();
        actual.Should().BeEquivalentTo(expectedCustomer)
    }

There is then an action that you can use your assertion framework 
to verify the object from the API matches your expected object.

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

        // Act
        var response = await client.GetAsync("api/tester/ReturnsJsonObject");

        // Assert
        await response.ShouldBeJson<Customer>(customer =>
            {
                // Add your checking code here using an Assertion framework such as FluentAssertions
                customer.Should().BeEquivalentTo(expectedCustomer)
                // or using XUnit
                Assert.Equal(expectedCustomer.Id, customer.Id);
            });
    }




# Feedback
Please feel free to leave feedback on my [GitHub page](https://github.com/ianbowyer/BowSoft).