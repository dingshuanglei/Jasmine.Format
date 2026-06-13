using Jasmine.Format.Elements.Basic;

namespace Jasmine.Format.Tests
{
    public class HtmlImgTests
    {
        [Fact]
        public void Constructor_WithSrc_ShouldCreateImage()
        {
            // Act
            var img = new HtmlImg("image.jpg");

            // Assert
            Assert.Equal("image.jpg", img.Src);
            Assert.Equal("<img src=\"image.jpg\" alt=\"\" />", img.ToHtml());
        }

        [Fact]
        public void Constructor_WithSrcAndAlt_ShouldCreateImageWithAlt()
        {
            // Act
            var img = new HtmlImg("image.jpg", "图片描述");

            // Assert
            Assert.Equal("图片描述", img.Alt);
            Assert.Equal("<img src=\"image.jpg\" alt=\"图片描述\" />", img.ToHtml());
        }

        [Fact]
        public void Constructor_WithDimensions_ShouldAddWidthHeight()
        {
            // Act
            var img = new HtmlImg("image.jpg", "描述", "100", "80");

            // Assert
            Assert.Equal("100", img.Width);
            Assert.Equal("80", img.Height);
            Assert.Contains("width=\"100\"", img.ToHtml());
            Assert.Contains("height=\"80\"", img.ToHtml());
        }

        [Fact]
        public void Constructor_WithStyle_ShouldAddStyle()
        {
            // Act
            var img = new HtmlImg("image.jpg", "描述", "100", "80", "border:1px solid #ccc;");

            // Assert
            Assert.Contains("style=\"border:1px solid #ccc;\"", img.ToHtml());
        }

        [Fact]
        public void Constructor_WithClass_ShouldAddClass()
        {
            // Act
            var img = new HtmlImg("image.jpg", "描述", "100", "80", null, "responsive-img");

            // Assert
            Assert.Contains("class=\"responsive-img\"", img.ToHtml());
        }

        [Fact]
        public void ToString_ShouldReturnHtml()
        {
            // Act
            var img = new HtmlImg("test.jpg");

            // Assert
            Assert.Equal(img.ToHtml(), img.ToString());
        }
    }
}