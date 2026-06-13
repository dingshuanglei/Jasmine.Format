using Jasmine.Format.Elements.List;
using Jasmine.Format.Elements.Text;

namespace Jasmine.Format.Tests
{
    public class HtmlUlTests
    {
        [Fact]
        public void Constructor_ShouldCreateEmptyUl()
        {
            // Act
            var ul = new HtmlList(ListType.Unordered);

            // Assert
            Assert.Equal("<ul></ul>", ul.ToHtml());
        }

        [Fact]
        public void Constructor_WithStyle_ShouldSetStyle()
        {
            // Act
            var ul = new HtmlList("list-style-type:square;", ListType.Unordered);

            // Assert
            Assert.Contains("style=\"list-style-type:square;\"", ul.ToHtml());
        }

        [Fact]
        public void AddItem_WithString_ShouldAddListItem()
        {
            // Act
            var ul = new HtmlList(ListType.Unordered).AddItem("项目一");

            // Assert
            Assert.Equal("<ul><li>项目一</li></ul>", ul.ToHtml());
        }

        [Fact]
        public void AddItem_MultipleItems_ShouldAddMultipleListItems()
        {
            // Act
            var ul = new HtmlList(ListType.Unordered)
                .AddItem("项目一")
                .AddItem("项目二")
                .AddItem("项目三");

            // Assert
            Assert.Equal("<ul><li>项目一</li><li>项目二</li><li>项目三</li></ul>", ul.ToHtml());
        }

        [Fact]
        public void AddItem_WithHtmlP_ShouldAddParagraphInLi()
        {
            // Act
            var ul = new HtmlList(ListType.Unordered).AddItem(new HtmlP().Add("段落内容"));

            // Assert
            Assert.Contains("<li><p>段落内容</p></li>", ul.ToHtml());
        }
    }
}