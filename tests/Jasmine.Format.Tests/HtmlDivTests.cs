using Jasmine.Format.Elements.Container;
using Jasmine.Format.Elements.Text;

namespace Jasmine.Format.Tests
{
    public class HtmlDivTests
    {
        [Fact]
        public void Constructor_ShouldCreateEmptyDiv()
        {
            // Act
            var div = new HtmlDiv();

            // Assert
            Assert.Equal("<div></div>", div.ToHtml());
        }

        [Fact]
        public void Constructor_WithStyle_ShouldSetStyle()
        {
            // Act
            var div = new HtmlDiv("border:1px solid #ccc;");

            // Assert
            Assert.Equal("border:1px solid #ccc;", div.Style);
            Assert.Contains("style=\"border:1px solid #ccc;\"", div.ToHtml());
        }

        [Fact]
        public void Add_WithString_ShouldAddParagraph()
        {
            // Act
            var div = new HtmlDiv().Add("内容");

            // Assert
            Assert.Equal("<div><p>内容</p></div>", div.ToHtml());
        }

        [Fact]
        public void Add_WithHtmlP_ShouldAddParagraph()
        {
            // Act
            var div = new HtmlDiv().Add(new HtmlP().Add("段落内容"));

            // Assert
            Assert.Equal("<div><p>段落内容</p></div>", div.ToHtml());
        }

        [Fact]
        public void AddMultiple_ShouldAddMultipleParagraphs()
        {
            // Act
            var div = new HtmlDiv()
                .Add("第一段")
                .Add("第二段");

            // Assert
            Assert.Equal("<div><p>第一段</p><p>第二段</p></div>", div.ToHtml());
        }

        [Fact]
        public void ToString_ShouldReturnHtml()
        {
            // Act
            var div = new HtmlDiv().Add("内容");

            // Assert
            Assert.Equal(div.ToHtml(), div.ToString());
        }
    }
}