using Jasmine.Format.Utilities;
using System.Collections.Generic;

namespace Jasmine.Format.Tests
{
    public class HtmlFormatHelperTests
    {
        [Fact]
        public void Format_ShouldThrowException_WhenTemplateIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => HtmlFormatHelper.Format(null));
        }

        [Fact]
        public void Format_ShouldThrowException_WhenTemplateIsEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => HtmlFormatHelper.Format(""));
        }

        [Fact]
        public void Format_ShouldReplacePlaceholders()
        {
            // Act
            var result = HtmlFormatHelper.Format("<p>Hello {0}, you are {1} years old</p>", "Alice", 25);

            // Assert
            Assert.Equal("<p>Hello Alice, you are 25 years old</p>", result);
        }

        [Fact]
        public void Format_ShouldEncodeSpecialCharacters()
        {
            // Act
            var result = HtmlFormatHelper.Format("<p>{0}</p>", "<b>bold</b>");

            // Assert
            Assert.Equal("<p>&lt;b&gt;bold&lt;/b&gt;</p>", result);
        }

        [Fact]
        public void Format_ShouldHandleNullArguments()
        {
            // Act
            var result = HtmlFormatHelper.Format("<p>{0}</p>", (object)null);

            // Assert
            Assert.Equal("<p></p>", result);
        }

        [Fact]
        public void FormatTemplate_WithKeyValuePairs_ShouldReplacePlaceholders()
        {
            // Arrange
            var keyValues = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("name", "Bob"),
                new KeyValuePair<string, string>("age", "30")
            };

            // Act
            var result = HtmlFormatHelper.FormatTemplate("<p>Hello {name}, age: {age}</p>", keyValues);

            // Assert
            Assert.Equal("<p>Hello Bob, age: 30</p>", result);
        }

        [Fact]
        public void FormatTemplate_ShouldThrowException_WhenTemplateIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => HtmlFormatHelper.FormatTemplate(null));
        }

        [Fact]
        public void FormatTemplate_ShouldReturnTemplate_WhenKeyValuesIsNull()
        {
            // Act
            var result = HtmlFormatHelper.FormatTemplate("<p>test</p>", (KeyValuePair<string, string>[])null);

            // Assert
            Assert.Equal("<p>test</p>", result);
        }

        [Fact]
        public void FormatTemplate_ShouldReturnTemplate_WhenKeyValuesIsEmpty()
        {
            // Act
            var result = HtmlFormatHelper.FormatTemplate("<p>test</p>", new KeyValuePair<string, string>[0]);

            // Assert
            Assert.Equal("<p>test</p>", result);
        }

        [Fact]
        public void FormatTemplate_ShouldEncodeSpecialCharacters_ByDefault()
        {
            // Arrange
            var keyValues = new[] { new KeyValuePair<string, string>("content", "<b>bold</b>") };

            // Act
            var result = HtmlFormatHelper.FormatTemplate("<p>{content}</p>", keyValues);

            // Assert
            Assert.Equal("<p>&lt;b&gt;bold&lt;/b&gt;</p>", result);
        }

        [Fact]
        public void FormatTemplate_ShouldNotEncode_WhenEscapeIsFalse()
        {
            // Arrange
            var keyValues = new[] { new KeyValuePair<string, string>("content", "<b>bold</b>") };

            // Act
            var result = HtmlFormatHelper.FormatTemplate("<p>{content}</p>", false, keyValues);

            // Assert
            Assert.Equal("<p><b>bold</b></p>", result);
        }

        [Fact]
        public void FormatTemplate_WithDictionary_ShouldReplacePlaceholders()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { "name", "Charlie" },
                { "age", "35" }
            };

            // Act
            var result = HtmlFormatHelper.FormatTemplate("<p>Hello {name}, age: {age}</p>", dict);

            // Assert
            Assert.Equal("<p>Hello Charlie, age: 35</p>", result);
        }

        [Fact]
        public void FormatTemplate_WithDictionary_ShouldHandleNullValue()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { "name", null },
                { "age", "25" }
            };

            // Act
            var result = HtmlFormatHelper.FormatTemplate("<p>Hello {name}, age: {age}</p>", dict);

            // Assert
            Assert.Equal("<p>Hello , age: 25</p>", result);
        }

        [Fact]
        public void FormatTemplate_WithDictionary_ShouldReturnTemplate_WhenDictionaryIsNull()
        {
            // Act
            var result = HtmlFormatHelper.FormatTemplate("<p>test</p>", (Dictionary<string, string>)null);

            // Assert
            Assert.Equal("<p>test</p>", result);
        }

        [Fact]
        public void FormatTemplate_ShouldHandleMultiplePlaceholders()
        {
            // Arrange
            var keyValues = new[]
            {
                new KeyValuePair<string, string>("title", "My Page"),
                new KeyValuePair<string, string>("author", "John"),
                new KeyValuePair<string, string>("date", "2024-01-01")
            };

            // Act
            var result = HtmlFormatHelper.FormatTemplate("<div><h1>{title}</h1><p>By {author} on {date}</p></div>", keyValues);

            // Assert
            Assert.Equal("<div><h1>My Page</h1><p>By John on 2024-01-01</p></div>", result);
        }
    }
}