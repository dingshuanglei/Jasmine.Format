using Jasmine.Format.Elements.Basic;
using Jasmine.Format.Elements.Text;
using Xunit;

namespace Jasmine.Format.Tests
{
    public class HtmlPTests
    {
        [Fact]
        public void AddSpanRange_WithValidItems_CreatesSpans()
        {
            // Arrange
            var items = new[] { "Item1", "Item2", "Item3" };
            
            // Act
            var p = new HtmlP().AddSpanRange(items, x => x, "blue");
            
            // Assert
            string html = p.ToHtml();
            Assert.Contains("<span style=\"color:blue;\">Item1</span>", html);
            Assert.Contains("<span style=\"color:blue;\">Item2</span>", html);
            Assert.Contains("<span style=\"color:blue;\">Item3</span>", html);
        }

        [Fact]
        public void AddSpanRange_WithSelector_CorrectlyTransformsItems()
        {
            // Arrange
            var items = new[] {
                new { Name = "Apple", Price = 10 },
                new { Name = "Banana", Price = 5 }
            };
            
            // Act
            var p = new HtmlP().AddSpanRange(items, x => $"{x.Name}: {x.Price}", "#ff6600");
            
            // Assert
            string html = p.ToHtml();
            Assert.Contains("<span style=\"color:#ff6600;\">Apple: 10</span>", html);
            Assert.Contains("<span style=\"color:#ff6600;\">Banana: 5</span>", html);
        }

        [Fact]
        public void AddSpanRange_WithStyle_AppliesStyleToAllSpans()
        {
            // Arrange
            var items = new[] { "A", "B" };
            
            // Act
            var p = new HtmlP().AddSpanRange(items, x => x, "red", "font-weight:bold;");
            
            // Assert
            string html = p.ToHtml();
            Assert.Contains("<span style=\"color:red;font-weight:bold;\">A</span>", html);
            Assert.Contains("<span style=\"color:red;font-weight:bold;\">B</span>", html);
        }

        [Fact]
        public void AddSpanRange_WithNullItems_ReturnsSameInstance()
        {
            // Arrange
            var p = new HtmlP().Add("test");
            
            // Act
            var result = p.AddSpanRange<string>(null, x => x.ToString(), "blue");
            
            // Assert
            Assert.Same(p, result);
        }

        [Fact]
        public void AddSpanRange_WithEmptyItems_ReturnsNewInstance()
        {
            // Arrange
            var items = Array.Empty<string>();
            var p = new HtmlP().Add("before");
            
            // Act
            var result = p.AddSpanRange(items, x => x, "blue");
            
            // Assert
            Assert.NotSame(p, result);
            Assert.Contains("before", result.ToHtml());
        }

        [Fact]
        public void AddSpanRange_ChainAfter_AddsContentBeforeSpans()
        {
            // Arrange
            var items = new[] { "Item1" };
            
            // Act
            var p = new HtmlP()
                .Add("Prefix: ")
                .AddSpanRange(items, x => x, "green");
            
            // Assert
            string html = p.ToHtml();
            Assert.Equal("<p>Prefix: <span style=\"color:green;\">Item1</span></p>", html);
        }

        [Fact]
        public void AddSpanRange_WithNullElementInItems_SkipsNull()
        {
            // Arrange
            var items = new string[] { "Valid", null, "Another" };
            
            // Act
            var p = new HtmlP().AddSpanRange(items, x => x, "blue");
            
            // Assert
            string html = p.ToHtml();
            Assert.Contains("Valid", html);
            Assert.DoesNotContain("null", html);
            Assert.Contains("Another", html);
        }

        [Fact]
        public void AddSpanRange_WithHtmlSpanSelector_CreatesSpans()
        {
            // Arrange
            var items = new[] { "Item1", "Item2" };
            
            // Act
            var p = new HtmlP().AddSpanRange(items, x => new HtmlSpan(x, "red"));
            
            // Assert
            string html = p.ToHtml();
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
            var p = new HtmlP().AddSpanRange(items, x => new HtmlSpan(x.Name, x.Status ? "green" : "red", "font-weight:bold;"));
            
            // Assert
            string html = p.ToHtml();
            Assert.Contains("<span style=\"color:green;font-weight:bold;\">正常</span>", html);
            Assert.Contains("<span style=\"color:red;font-weight:bold;\">异常</span>", html);
        }

        [Fact]
        public void AddSpanRange_WithHtmlSpanSelector_NullSpanSkips()
        {
            // Arrange
            var items = new[] { "A", null, "B" };
            
            // Act
            var p = new HtmlP().AddSpanRange(items, x => x == null ? null : new HtmlSpan(x, "blue"));
            
            // Assert
            string html = p.ToHtml();
            Assert.Contains("A", html);
            Assert.DoesNotContain("null", html);
            Assert.Contains("B", html);
        }

        [Fact]
        public void AddRawRange_WithMultipleSpans_CreatesCorrectHtml()
        {
            // Arrange
            var items = new[] { "Item1", "Item2" };
            
            // Act
            var p = new HtmlP().AddRawRange(items, x => $"{new HtmlSpan(x, "red")}开{new HtmlSpan(x + "-no", "blue")}");
            
            // Assert
            string html = p.ToHtml();
            Assert.Contains("<span style=\"color:red;\">Item1</span>开<span style=\"color:blue;\">Item1-no</span>", html);
            Assert.Contains("<span style=\"color:red;\">Item2</span>开<span style=\"color:blue;\">Item2-no</span>", html);
        }

        [Fact]
        public void AddRawRange_WithDynamicStyle_CreatesCorrectHtml()
        {
            // Arrange
            var items = new[] {
                new { Name = "正常", Status = true },
                new { Name = "异常", Status = false }
            };
            
            // Act
            var p = new HtmlP().AddRawRange(items, x => $"{new HtmlSpan(x.Name, x.Status ? "green" : "red")}");
            
            // Assert
            string html = p.ToHtml();
            Assert.Contains("<span style=\"color:green;\">正常</span>", html);
            Assert.Contains("<span style=\"color:red;\">异常</span>", html);
        }

        [Fact]
        public void AddRawRange_WithNullItems_ReturnsSameInstance()
        {
            // Arrange
            var p = new HtmlP().Add("test");
            
            // Act
            var result = p.AddRawRange<string>(null, x => x);
            
            // Assert
            Assert.Same(p, result);
        }

        [Fact]
        public void Add_WithText_AddsContent()
        {
            // Act
            var p = new HtmlP().Add("Hello World");
            
            // Assert
            Assert.Equal("<p>Hello World</p>", p.ToHtml());
        }

        [Fact]
        public void Add_WithEscapeFalse_DoesNotEncode()
        {
            // Act
            var p = new HtmlP().Add("<script>alert('xss')</script>", false);
            
            // Assert
            Assert.Contains("<script>alert('xss')</script>", p.ToHtml());
        }

        [Fact]
        public void Add_WithEscapeTrue_EncodesHtml()
        {
            // Act
            var p = new HtmlP().Add("<script>alert('xss')</script>", true);
            
            // Assert
            Assert.Contains("&lt;script&gt;alert(&#39;xss&#39;)&lt;/script&gt;", p.ToHtml());
        }

        [Fact]
        public void Add_WithHtmlSpan_AddsSpan()
        {
            // Act
            var p = new HtmlP().Add("Name: ").Add(new HtmlSpan("John", "blue"));
            
            // Assert
            Assert.Equal("<p>Name: <span style=\"color:blue;\">John</span></p>", p.ToHtml());
        }

        [Fact]
        public void Add_WithHtmlA_AddsLink()
        {
            // Act
            var p = new HtmlP().Add("Visit ").Add(new HtmlA("example", "https://example.com"));
            
            // Assert
            Assert.Contains("<a href=\"https://example.com\">example</a>", p.ToHtml());
        }

        [Fact]
        public void AddRaw_AddsRawHtml()
        {
            // Act
            var p = new HtmlP().AddRaw("<br/>").Add("After");
            
            // Assert
            Assert.Equal("<p><br/>After</p>", p.ToHtml());
        }

        [Fact]
        public void Clear_ReturnsEmptyParagraph()
        {
            // Arrange
            var p = new HtmlP().Add("content");
            
            // Act
            var result = p.Clear();
            
            // Assert
            Assert.Equal("<p></p>", result.ToHtml());
            Assert.NotSame(p, result);
        }

        [Fact]
        public void WithStyle_AddsStyleToParagraph()
        {
            // Arrange
            var p = new HtmlP().Add("text");
            
            // Act
            var result = p.WithStyle("color:red;");
            
            // Assert
            Assert.Equal("<p style=\"color:red;\">text</p>", result.ToHtml());
            Assert.NotSame(p, result);
        }

        [Fact]
        public void Count_ReturnsElementCount()
        {
            // Act
            var p = new HtmlP().Add("a").Add("b").Add("c");
            
            // Assert
            Assert.Equal(3, p.Count);
        }

        [Fact]
        public void IsEmpty_ReturnsTrueForEmpty()
        {
            // Act
            var p1 = new HtmlP();
            var p2 = new HtmlP().Add("content");
            
            // Assert
            Assert.True(p1.IsEmpty);
            Assert.False(p2.IsEmpty);
        }

        [Fact]
        public void ClearStyle_ReturnsNewInstanceWithoutStyle()
        {
            // Arrange
            var p = new HtmlP().Add("content").WithStyle("color:red; font-size:14px;");
            
            // Act
            var result = p.ClearStyle();
            
            // Assert
            Assert.NotSame(p, result);
            Assert.Equal("<p>content</p>", result.ToHtml());
            Assert.Null(result.Style);
        }

        [Fact]
        public void ClearStyle_PreservesContent()
        {
            // Arrange
            var p = new HtmlP()
                .Add("Hello ")
                .Add(new HtmlSpan("World", "blue"))
                .WithStyle("text-align:center;");
            
            // Act
            var result = p.ClearStyle();
            
            // Assert
            Assert.Contains("Hello", result.ToHtml());
            Assert.Contains("<span style=\"color:blue;\">World</span>", result.ToHtml());
            Assert.DoesNotContain("text-align:center", result.ToHtml());
        }

        [Fact]
        public void ClearStyle_ChainedCall_WorksCorrectly()
        {
            // Act
            var p = new HtmlP()
                .Add("text")
                .WithStyle("color:red;")
                .ClearStyle();
            
            // Assert
            Assert.Equal("<p>text</p>", p.ToHtml());
        }

        [Fact]
        public void ToPlainText_ReturnsPlainTextWithoutTags()
        {
            // Arrange
            var p = new HtmlP().Add("Hello World");
            
            // Act
            string result = p.ToPlainText();
            
            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void ToPlainText_WithSpan_ReturnsTextOnly()
        {
            // Arrange
            var p = new HtmlP()
                .Add("Hello ")
                .Add(new HtmlSpan("World", "blue"))
                .Add("!");
            
            // Act
            string result = p.ToPlainText();
            
            // Assert
            Assert.Equal("Hello World!", result);
        }

        [Fact]
        public void ToPlainText_WithLink_ReturnsLinkText()
        {
            // Arrange
            var p = new HtmlP()
                .Add("Visit ")
                .Add(new HtmlA("example", "https://example.com"));
            
            // Act
            string result = p.ToPlainText();
            
            // Assert
            Assert.Equal("Visit example", result);
        }

        [Fact]
        public void ToPlainText_WithImage_ReturnsAltText()
        {
            // Arrange
            var p = new HtmlP()
                .Add("Image: ")
                .Add(new HtmlImg("https://example.com/img.jpg", "示例图片"));
            
            // Act
            string result = p.ToPlainText();
            
            // Assert
            Assert.Equal("Image: 示例图片", result);
        }

        [Fact]
        public void ToPlainText_EmptyParagraph_ReturnsEmptyString()
        {
            // Arrange
            var p = new HtmlP();
            
            // Act
            string result = p.ToPlainText();
            
            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ToPlainText_ComplexContent_ReturnsConcatenatedText()
        {
            // Arrange
            var p = new HtmlP()
                .Add("User: ")
                .Add(new HtmlSpan("John", "#0066cc"))
                .Add(" - ")
                .Add(new HtmlA("Profile", "https://example.com/profile"))
                .Add(" - ")
                .Add(new HtmlImg("avatar.jpg", "Avatar"));
            
            // Act
            string result = p.ToPlainText();
            
            // Assert
            Assert.Equal("User: John - Profile - Avatar", result);
        }
    }
}