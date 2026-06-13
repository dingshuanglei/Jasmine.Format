using Jasmine.Format.Elements.Basic;

namespace Jasmine.Format.Tests
{
    public class HtmlATests
    {
        [Fact]
        public void Constructor_WithHref_ShouldCreateLink()
        {
            // Act
            var link = new HtmlA("点击这里", "https://example.com");

            // Assert
            Assert.Equal("点击这里", link.Content);
            Assert.Equal("https://example.com", link.Href);
            Assert.Equal("<a href=\"https://example.com\">点击这里</a>", link.ToHtml());
        }

        [Fact]
        public void Constructor_WithColor_ShouldAddColorStyle()
        {
            // Act
            var link = new HtmlA("链接", "https://example.com", color: "#0066cc");

            // Assert
            Assert.Equal("#0066cc", link.Color);
            Assert.Contains("color:#0066cc;", link.ToHtml());
        }

        [Fact]
        public void Constructor_WithTarget_ShouldAddTarget()
        {
            // Act
            var link = new HtmlA("新窗口", "https://example.com", "_blank");

            // Assert
            Assert.Equal("_blank", link.Target);
            Assert.Contains("target=\"_blank\"", link.ToHtml());
        }

        [Fact]
        public void Constructor_WithStyle_ShouldAddStyle()
        {
            // Act
            var link = new HtmlA("链接", "https://example.com", color: "#0066cc", style: "text-decoration:none;");

            // Assert
            Assert.Equal("text-decoration:none;", link.Style);
            Assert.Contains("text-decoration:none;", link.ToHtml());
        }

        [Fact]
        public void ToString_ShouldReturnHtml()
        {
            // Act
            var link = new HtmlA("测试", "https://test.com");

            // Assert
            Assert.Equal(link.ToHtml(), link.ToString());
        }

        [Fact]
        public void Constructor_WithTargetAndColor_ShouldSetBoth()
        {
            // Act
            var link = new HtmlA("红色链接", "https://example.com", "_blank", "red");

            // Assert
            Assert.Equal("_blank", link.Target);
            Assert.Equal("red", link.Color);
            Assert.Contains("target=\"_blank\"", link.ToHtml());
            Assert.Contains("color:red;", link.ToHtml());
        }

        [Fact]
        public void WithTarget_ShouldSetTargetAttribute()
        {
            // Arrange
            var link = new HtmlA("链接", "https://example.com");

            // Act
            var newLink = link.WithTarget("_blank");

            // Assert
            Assert.Equal("_blank", newLink.Target);
            Assert.Contains("target=\"_blank\"", newLink.ToHtml());
            Assert.Equal("链接", newLink.Content);
            Assert.Equal("https://example.com", newLink.Href);
        }

        [Fact]
        public void WithColor_ShouldSetColorStyle()
        {
            // Arrange
            var link = new HtmlA("链接", "https://example.com");

            // Act
            var newLink = link.WithColor("#ff0000");

            // Assert
            Assert.Equal("#ff0000", newLink.Color);
            Assert.Contains("color:#ff0000;", newLink.ToHtml());
            Assert.Equal("链接", newLink.Content);
            Assert.Equal("https://example.com", newLink.Href);
        }

        [Fact]
        public void WithStyle_ShouldSetCustomStyle()
        {
            // Arrange
            var link = new HtmlA("链接", "https://example.com");

            // Act
            var newLink = link.WithStyle("font-weight:bold;");

            // Assert
            Assert.Equal("font-weight:bold;", newLink.Style);
            Assert.Contains("font-weight:bold;", newLink.ToHtml());
            Assert.Equal("链接", newLink.Content);
            Assert.Equal("https://example.com", newLink.Href);
        }
    }
}