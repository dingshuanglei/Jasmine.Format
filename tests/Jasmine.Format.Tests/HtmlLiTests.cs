using Jasmine.Format.Elements.List;
using Jasmine.Format.Elements.Text;
using Xunit;

namespace Jasmine.Format.Tests
{
    public class HtmlLiTests
    {
        [Fact]
        public void AddSpanRange_WithValidItems_CreatesSpans()
        {
            // Arrange
            var items = new[] { "Item1", "Item2", "Item3" };
            
            // Act
            var li = new HtmlLi().AddSpanRange(items, x => x, "blue");
            
            // Assert
            string html = li.ToHtml();
            Assert.Contains("<span style=\"color:blue;\">Item1</span>", html);
            Assert.Contains("<span style=\"color:blue;\">Item2</span>", html);
            Assert.Contains("<span style=\"color:blue;\">Item3</span>", html);
        }

        [Fact]
        public void AddSpanRange_WithSelector_CorrectlyTransformsItems()
        {
            // Arrange
            var items = new[] {
                new { Station = "北京", TrainNo = "G123" },
                new { Station = "上海", TrainNo = "G456" }
            };
            
            // Act
            var li = new HtmlLi().AddSpanRange(items, x => $"{x.Station}开{x.TrainNo}", "#0066cc");
            
            // Assert
            string html = li.ToHtml();
            Assert.Contains("<span style=\"color:#0066cc;\">北京开G123</span>", html);
            Assert.Contains("<span style=\"color:#0066cc;\">上海开G456</span>", html);
        }

        [Fact]
        public void AddSpanRange_WithStyle_AppliesStyleToAllSpans()
        {
            // Arrange
            var items = new[] { "A", "B" };
            
            // Act
            var li = new HtmlLi().AddSpanRange(items, x => x, "red", "font-weight:bold;");
            
            // Assert
            string html = li.ToHtml();
            Assert.Contains("<span style=\"color:red;font-weight:bold;\">A</span>", html);
            Assert.Contains("<span style=\"color:red;font-weight:bold;\">B</span>", html);
        }

        [Fact]
        public void AddSpanRange_WithNullItems_ReturnsSameInstance()
        {
            // Arrange
            var li = new HtmlLi("test");
            
            // Act
            var result = li.AddSpanRange<string>(null, x => x.ToString(), "blue");
            
            // Assert
            Assert.Same(li, result);
        }

        [Fact]
        public void AddSpanRange_WithEmptyItems_ReturnsNewInstance()
        {
            // Arrange
            var items = Array.Empty<string>();
            var li = new HtmlLi("before");
            
            // Act
            var result = li.AddSpanRange(items, x => x, "blue");
            
            // Assert
            Assert.NotSame(li, result);
            Assert.Contains("before", result.ToHtml());
        }

        [Fact]
        public void AddSpanRange_ChainAfter_AddsContentBeforeSpans()
        {
            // Arrange
            var items = new[] { "Item1" };
            
            // Act
            var li = new HtmlLi()
                .Add("Prefix: ")
                .AddSpanRange(items, x => x, "green");
            
            // Assert
            string html = li.ToHtml();
            Assert.Equal("<li>Prefix: <span style=\"color:green;\">Item1</span></li>", html);
        }

        [Fact]
        public void AddSpanRange_WithNullElementInItems_SkipsNull()
        {
            // Arrange
            var items = new string[] { "Valid", null, "Another" };
            
            // Act
            var li = new HtmlLi().AddSpanRange(items, x => x, "blue");
            
            // Assert
            string html = li.ToHtml();
            Assert.Contains("Valid", html);
            Assert.DoesNotContain("null", html);
            Assert.Contains("Another", html);
        }

        [Fact]
        public void AddSpanRange_InList_CreatesNestedStructure()
        {
            // Arrange
            var items = new[] { "A", "B" };
            var li = new HtmlLi().AddSpanRange(items, x => x, "green");
            
            // Act
            var list = new HtmlList(ListType.Unordered).AddItem(li);
            
            // Assert
            string html = list.ToHtml();
            Assert.Contains("<ul><li><span style=\"color:green;\">A</span><span style=\"color:green;\">B</span></li></ul>", html);
        }

        [Fact]
        public void AddSpanRange_WithHtmlSpanSelector_CreatesSpans()
        {
            // Arrange
            var items = new[] { "Item1", "Item2" };
            
            // Act
            var li = new HtmlLi().AddSpanRange(items, x => new HtmlSpan(x, "red"));
            
            // Assert
            string html = li.ToHtml();
            Assert.Contains("<span style=\"color:red;\">Item1</span>", html);
            Assert.Contains("<span style=\"color:red;\">Item2</span>", html);
        }

        [Fact]
        public void AddSpanRange_WithHtmlSpanSelector_DynamicStyle()
        {
            // Arrange
            var items = new[] {
                new { Name = "正常", Status = true },
                new { Name = "异常", Status = false }
            };
            
            // Act
            var li = new HtmlLi().AddSpanRange(items, x => new HtmlSpan(x.Name, x.Status ? "green" : "red", "font-weight:bold;"));
            
            // Assert
            string html = li.ToHtml();
            Assert.Contains("<span style=\"color:green;font-weight:bold;\">正常</span>", html);
            Assert.Contains("<span style=\"color:red;font-weight:bold;\">异常</span>", html);
        }

        [Fact]
        public void AddRawRange_WithMultipleSpans_CreatesCorrectHtml()
        {
            // Arrange
            var items = new[] { "Item1", "Item2" };
            
            // Act
            var li = new HtmlLi().AddRawRange(items, x => $"{new HtmlSpan(x, "red")}开{new HtmlSpan(x + "-no", "blue")}");
            
            // Assert
            string html = li.ToHtml();
            Assert.Contains("<span style=\"color:red;\">Item1</span>开<span style=\"color:blue;\">Item1-no</span>", html);
            Assert.Contains("<span style=\"color:red;\">Item2</span>开<span style=\"color:blue;\">Item2-no</span>", html);
        }

        [Fact]
        public void AddRawRange_WithNullItems_ReturnsSameInstance()
        {
            // Arrange
            var li = new HtmlLi("test");
            
            // Act
            var result = li.AddRawRange<string>(null, x => x);
            
            // Assert
            Assert.Same(li, result);
        }

        [Fact]
        public void Add_WithText_AddsContent()
        {
            // Act
            var li = new HtmlLi().Add("List Item");
            
            // Assert
            Assert.Equal("<li>List Item</li>", li.ToHtml());
        }

        [Fact]
        public void Add_WithEscapeFalse_DoesNotEncode()
        {
            // Act
            var li = new HtmlLi().Add("<script>alert('xss')</script>", false);
            
            // Assert
            Assert.Contains("<script>alert('xss')</script>", li.ToHtml());
        }

        [Fact]
        public void Add_WithHtmlSpan_AddsSpan()
        {
            // Act
            var li = new HtmlLi().Add("Name: ").Add(new HtmlSpan("John", "blue"));
            
            // Assert
            Assert.Equal("<li>Name: <span style=\"color:blue;\">John</span></li>", li.ToHtml());
        }

        [Fact]
        public void Add_WithHtmlP_AddsParagraph()
        {
            // Act
            var li = new HtmlLi().Add(new HtmlP().Add("Nested paragraph"));
            
            // Assert
            Assert.Contains("<p>Nested paragraph</p>", li.ToHtml());
        }

        [Fact]
        public void AddRaw_AddsRawHtml()
        {
            // Act
            var li = new HtmlLi().AddRaw("<br/>").Add("After");
            
            // Assert
            Assert.Equal("<li><br/>After</li>", li.ToHtml());
        }

        [Fact]
        public void Clear_ReturnsEmptyListItem()
        {
            // Arrange
            var li = new HtmlLi("content");
            
            // Act
            var result = li.Clear();
            
            // Assert
            Assert.Equal("<li></li>", result.ToHtml());
            Assert.NotSame(li, result);
        }

        [Fact]
        public void WithStyle_AddsStyleToListItem()
        {
            // Arrange
            var li = new HtmlLi("text");
            
            // Act
            var result = li.WithStyle("color:red;");
            
            // Assert
            Assert.Equal("<li style=\"color:red;\">text</li>", result.ToHtml());
            Assert.NotSame(li, result);
        }

        [Fact]
        public void Count_ReturnsElementCount()
        {
            // Act
            var li = new HtmlLi().Add("a").Add("b").Add("c");
            
            // Assert
            Assert.Equal(3, li.Count);
        }

        [Fact]
        public void IsEmpty_ReturnsTrueForEmpty()
        {
            // Act
            var li1 = new HtmlLi();
            var li2 = new HtmlLi("content");
            
            // Assert
            Assert.True(li1.IsEmpty);
            Assert.False(li2.IsEmpty);
        }

        [Fact]
        public void Constructor_WithText_CreatesListItem()
        {
            // Act
            var li = new HtmlLi("Hello");
            
            // Assert
            Assert.Equal("<li>Hello</li>", li.ToHtml());
        }

        [Fact]
        public void Constructor_WithTextAndEscapeFalse_DoesNotEncode()
        {
            // Act
            var li = new HtmlLi("<b>bold</b>", false);
            
            // Assert
            Assert.Equal("<li><b>bold</b></li>", li.ToHtml());
        }
    }
}