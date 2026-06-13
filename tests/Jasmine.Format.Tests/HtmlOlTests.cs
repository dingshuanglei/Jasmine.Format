using Jasmine.Format.Elements.List;
using Jasmine.Format.Elements.Text;

namespace Jasmine.Format.Tests
{
    public class HtmlOlTests
    {
        [Fact]
        public void Constructor_ShouldCreateEmptyOl()
        {
            // Act
            var ol = new HtmlList(ListType.Ordered);

            // Assert
            Assert.Equal("<ol></ol>", ol.ToHtml());
        }

        [Fact]
        public void Constructor_WithStart_ShouldSetStartAttribute()
        {
            // Act
            var ol = new HtmlList(ListType.Ordered, 5);

            // Assert
            Assert.Contains("start=\"5\"", ol.ToHtml());
        }

        [Fact]
        public void AddItem_WithString_ShouldAddListItem()
        {
            // Act
            var ol = new HtmlList(ListType.Ordered).AddItem("步骤一");

            // Assert
            Assert.Equal("<ol><li>步骤一</li></ol>", ol.ToHtml());
        }

        [Fact]
        public void AddItem_MultipleItems_ShouldAddMultipleListItems()
        {
            // Act
            var ol = new HtmlList(ListType.Ordered)
                .AddItem("步骤一")
                .AddItem("步骤二")
                .AddItem("步骤三");

            // Assert
            Assert.Equal("<ol><li>步骤一</li><li>步骤二</li><li>步骤三</li></ol>", ol.ToHtml());
        }

        [Fact]
        public void AddItem_WithHtmlP_ShouldAddParagraphInLi()
        {
            // Act
            var ol = new HtmlList(ListType.Ordered).AddItem(new HtmlP().Add("段落内容"));

            // Assert
            Assert.Contains("<li><p>段落内容</p></li>", ol.ToHtml());
        }
    }
}