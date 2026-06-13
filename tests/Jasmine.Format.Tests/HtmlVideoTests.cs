using Jasmine.Format.Elements.Media;

namespace Jasmine.Format.Tests
{
    public class HtmlVideoTests
    {
        [Fact]
        public void Constructor_WithSrc_ShouldCreateVideo()
        {
            // Act
            var video = new HtmlVideo("video.mp4");

            // Assert
            Assert.Equal("video.mp4", video.Src);
            Assert.True(video.Controls);
            Assert.Contains("<video controls>", video.ToHtml());
        }

        [Fact]
        public void Constructor_WithDimensions_ShouldAddWidthHeight()
        {
            // Act
            var video = new HtmlVideo("video.mp4", "640", "480");

            // Assert
            Assert.Equal("640", video.Width);
            Assert.Equal("480", video.Height);
            Assert.Contains("width=\"640\"", video.ToHtml());
            Assert.Contains("height=\"480\"", video.ToHtml());
        }

        [Fact]
        public void Constructor_WithPoster_ShouldAddPoster()
        {
            // Act
            var video = new HtmlVideo("video.mp4", poster: "poster.jpg");

            // Assert
            Assert.Equal("poster.jpg", video.Poster);
            Assert.Contains("poster=\"poster.jpg\"", video.ToHtml());
        }

        [Fact]
        public void Constructor_WithAutoplayLoopMuted_ShouldAddAttributes()
        {
            // Act
            var video = new HtmlVideo("video.mp4", autoplay: true, loop: true, muted: true);

            // Assert
            Assert.True(video.Autoplay);
            Assert.True(video.Loop);
            Assert.True(video.Muted);
            Assert.Contains("autoplay", video.ToHtml());
            Assert.Contains("loop", video.ToHtml());
            Assert.Contains("muted", video.ToHtml());
        }

        [Fact]
        public void Constructor_WithNoControls_ShouldNotAddControls()
        {
            // Act
            var video = new HtmlVideo("video.mp4", controls: false);

            // Assert
            Assert.False(video.Controls);
            Assert.DoesNotContain("controls", video.ToHtml());
        }

        [Fact]
        public void ToString_ShouldReturnHtml()
        {
            // Act
            var video = new HtmlVideo("test.mp4");

            // Assert
            Assert.Equal(video.ToHtml(), video.ToString());
        }
    }
}