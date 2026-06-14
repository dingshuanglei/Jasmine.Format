using Jasmine.Format.Utilities;
using Xunit;

namespace Jasmine.Format.Tests
{
    public class HtmlTextExtractorTests
    {
        [Fact]
        public void ExtractPlainText_EmptyString_ReturnsEmpty()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("");
            
            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ExtractPlainText_NullString_ReturnsEmpty()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText(null);
            
            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ExtractPlainText_SimpleText_ReturnsSame()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("Hello World");
            
            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void ExtractPlainText_SingleTag_StripsTag()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<p>Hello</p>");
            
            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void ExtractPlainText_MultipleTags_StripsAllTags()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<div><p>Hello <span>World</span></p></div>");
            
            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void ExtractPlainText_Link_StripsTagKeepsText()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<a href=\"https://example.com\">Click here</a>");
            
            // Assert
            Assert.Equal("Click here", result);
        }

        [Fact]
        public void ExtractPlainText_Image_ReturnsEmpty()
        {
            // Note: img tags don't have closing text content, so nothing is extracted
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<img src=\"image.jpg\" alt=\"Picture\" />");
            
            // Assert
            Assert.Equal("", result); // img tag has no text content
        }

        [Fact]
        public void ExtractPlainText_DecodesHtmlEntities()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<p>&lt;script&gt; &amp; &quot;quotes&quot;</p>");
            
            // Assert
            Assert.Equal("<script> & \"quotes\"", result);
        }

        [Fact]
        public void ExtractPlainText_NumericEntity_DecodesCorrectly()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<p>&#39;single&#39;</p>");
            
            // Assert
            Assert.Equal("'single'", result);
        }

        [Fact]
        public void ExtractPlainText_HexEntity_DecodesCorrectly()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<p>&#x27;hex&#x27;</p>");
            
            // Assert
            Assert.Equal("'hex'", result);
        }

        [Fact]
        public void ExtractPlainText_ScriptTag_SkipsContent()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<script>alert('test')</script><p>Hello</p>");
            
            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void ExtractPlainText_StyleTag_SkipsContent()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<style>body { color: red; }</style><p>Hello</p>");
            
            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void ExtractPlainText_NbspEntity_DecodesToSpace()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<p>Hello&nbsp;World</p>");
            
            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void ExtractPlainText_ComplexHtml_ReturnsCleanText()
        {
            // Arrange
            string html = "<div class=\"container\"><h1>Title</h1><p>Paragraph with <strong>bold</strong> and <em>italic</em> text.</p><a href=\"#\">Link</a></div>";
            
            // Act
            string result = HtmlTextExtractor.ExtractPlainText(html);
            
            // Assert
            Assert.Equal("TitleParagraph with bold and italic text.Link", result);
        }

        [Fact]
        public void ExtractPlainText_SpecialEntities_DecodesCorrectly()
        {
            // Act
            string result = HtmlTextExtractor.ExtractPlainText("<p>&copy; &reg; &trade;</p>");
            
            // Assert
            Assert.Equal("© ® ™", result);
        }
    }
}