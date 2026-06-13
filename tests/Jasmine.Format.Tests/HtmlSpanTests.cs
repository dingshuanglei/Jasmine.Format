using Jasmine.Format.Elements.Text;

namespace Jasmine.Format.Tests
{
    public class HtmlSpanTests
    {
        [Fact]
        public void Constructor_ShouldInitializeContent_WhenContentIsNull()
        {
            // Act
            var span = new HtmlSpan(null);

            // Assert
            Assert.Equal(string.Empty, span.Content);
        }

        [Fact]
        public void Constructor_ShouldInitializeContent_WhenContentIsProvided()
        {
            // Act
            var span = new HtmlSpan("test content");

            // Assert
            Assert.Equal("test content", span.Content);
        }

        [Fact]
        public void Constructor_ShouldSetColor()
        {
            // Act
            var span = new HtmlSpan("content", "#ff0000");

            // Assert
            Assert.Equal("#ff0000", span.Color);
        }

        [Fact]
        public void Constructor_ShouldSetStyle()
        {
            // Act
            var span = new HtmlSpan("content", "#ff0000", "font-weight: bold;");

            // Assert
            Assert.Equal("#ff0000", span.Color);
            Assert.Equal("font-weight: bold;", span.Style);
        }

        [Fact]
        public void Constructor_ShouldSetStyleOnly_WhenColorIsNull()
        {
            // Act
            var span = new HtmlSpan("content", style: "font-size:14px;");

            // Assert
            Assert.Null(span.Color);
            Assert.Equal("font-size:14px;", span.Style);
        }

        [Fact]
        public void ToHtml_ShouldGenerateCorrectHtml_WithoutStyle()
        {
            // Arrange
            var span = new HtmlSpan("test");

            // Act
            var result = span.ToHtml();

            // Assert
            Assert.Equal("<span>test</span>", result);
        }

        [Fact]
        public void ToHtml_ShouldGenerateCorrectHtml_WithColor()
        {
            // Arrange
            var span = new HtmlSpan("test", "#ff0000");

            // Act
            var result = span.ToHtml();

            // Assert
            Assert.Equal("<span style=\"color:#ff0000;\">test</span>", result);
        }

        [Fact]
        public void ToHtml_ShouldGenerateCorrectHtml_WithColorAndStyle()
        {
            // Arrange
            var span = new HtmlSpan("test", "#ff0000", "font-weight: bold;");

            // Act
            var result = span.ToHtml();

            // Assert
            Assert.Equal("<span style=\"color:#ff0000;font-weight: bold;\">test</span>", result);
        }

        [Fact]
        public void ToHtml_ShouldGenerateCorrectHtml_WithStyleOnly()
        {
            // Arrange
            var span = new HtmlSpan("test", style: "font-size:14px;");

            // Act
            var result = span.ToHtml();

            // Assert
            Assert.Equal("<span style=\"font-size:14px;\">test</span>", result);
        }

        [Fact]
        public void ToHtml_ShouldEncodeContent_ByDefault()
        {
            // Arrange
            var span = new HtmlSpan("<b>bold text</b>");

            // Act
            var result = span.ToHtml();

            // Assert
            Assert.Equal("<span>&lt;b&gt;bold text&lt;/b&gt;</span>", result);
        }

        [Fact]
        public void ToHtml_ShouldNotEncodeContent_WhenEscapeIsFalse()
        {
            // Arrange
            var span = new HtmlSpan("<b>bold</b>");

            // Act
            var result = span.ToHtml(false);

            // Assert
            Assert.Equal("<span><b>bold</b></span>", result);
        }

        [Fact]
        public void ToHtml_ShouldUseDoubleQuotes_ForStyleAttribute()
        {
            // Arrange
            var span = new HtmlSpan("test", "#ff0000");

            // Act
            var result = span.ToHtml();

            // Assert
            Assert.Contains("style=\"", result);
            Assert.DoesNotContain("style='", result);
        }
    }
}