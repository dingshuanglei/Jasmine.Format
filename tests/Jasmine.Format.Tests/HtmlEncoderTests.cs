using Jasmine.Format.Utilities;

namespace Jasmine.Format.Tests
{
    public class HtmlEncoderTests
    {
        [Fact]
        public void HtmlEncode_ShouldReturnEmptyString_WhenInputIsNull()
        {
            // Act
            var result = typeof(HtmlEncoder)
                .GetMethod("HtmlEncode", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                ?.Invoke(null, new object[] { null });

            // Assert
            Assert.Equal(null, result);
        }

        [Fact]
        public void HtmlEncode_ShouldReturnEmptyString_WhenInputIsEmpty()
        {
            // Act
            var result = typeof(HtmlEncoder)
                .GetMethod("HtmlEncode", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                ?.Invoke(null, new object[] { string.Empty });

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void HtmlEncode_ShouldEncodeSpecialCharacters()
        {
            // Arrange
            var testCases = new[]
            {
                ("<", "&lt;"),
                (">", "&gt;"),
                ("&", "&amp;"),
                ("\"", "&quot;"),
                ("'", "&#39;"),
                ("<div>", "&lt;div&gt;"),
                ("Hello <World>", "Hello &lt;World&gt;")
            };

            // Act & Assert
            foreach (var (input, expected) in testCases)
            {
                var result = typeof(HtmlEncoder)
                    .GetMethod("HtmlEncode", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                    ?.Invoke(null, new object[] { input });

                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public void HtmlEncode_ShouldNotEncodeNormalText()
        {
            // Arrange
            string input = "Hello World!";

            // Act
            var result = typeof(HtmlEncoder)
                .GetMethod("HtmlEncode", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                ?.Invoke(null, new object[] { input });

            // Assert
            Assert.Equal(input, result);
        }
    }
}