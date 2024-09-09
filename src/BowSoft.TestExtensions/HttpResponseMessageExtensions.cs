using FluentAssertions;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BowSoft.TestExtensions
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Asserts if the content is a valid Json string.
        /// </summary>
        /// <param name="message">The Http Response Message.</param>
        /// <returns>True if the json can be parsed, false if it failed to parse the string.</returns>
        public static async Task<bool> ShouldBeJson(
            this HttpResponseMessage message)
        {
            try
            {
                var content = await message.ShouldBeBody();

                using (JsonDocument doc = JsonDocument.Parse(content))
                {
                    // dispose any created doc
                }
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the body of an HttpResponseMessage./>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task<string> ShouldBeBody(
           this HttpResponseMessage message)
        {
            message.Content.Should().NotBeNull();
            var content = message.Content.ReadAsStringAsync();
            content.Should().NotBeNull();

            return content;
        }

        public static async Task<T> ShouldBeJson<T>(
            this HttpResponseMessage message,
            Action<T> obj)
        {
            var content = await message.ShouldBeBody();
            var result = ShouldBeOfType<T>(content);
            obj.Invoke(result);
            return result;
        }

        public static async Task<T> ShouldBeJson<T>(
            this HttpResponseMessage message)
        {
            var content = await message.ShouldBeBody();
            var result = ShouldBeOfType<T>(content);
            return result;
        }

        public static async Task<T> ShouldBeJson<T>(
            this HttpResponseMessage message,
            T equivalentTo)
        {
            var content = await message.ShouldBeBody();
            var result = ShouldBeOfType<T>(content);

            result.Should().BeEquivalentTo(equivalentTo);
            return result;
        }

        public static void ShouldBeContentType(
            this HttpResponseMessage message,
            string contentType)
        {
            message.Content.Headers.ContentType.MediaType.Should().Be(contentType);
        }

        private static T ShouldBeOfType<T>(string content)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var result = JsonSerializer.Deserialize<T>(content, options);
            result.Should().NotBeNull();
            return result;
        }
    }
}