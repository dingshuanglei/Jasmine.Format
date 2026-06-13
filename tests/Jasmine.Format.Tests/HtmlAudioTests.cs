using Jasmine.Format.Elements.Media;

namespace Jasmine.Format.Tests
{
    public class HtmlAudioTests
    {
        [Fact]
        public void Constructor_WithSrc_ShouldCreateAudio()
        {
            // Act
            var audio = new HtmlAudio("audio.mp3");

            // Assert
            Assert.Equal("audio.mp3", audio.Src);
            Assert.True(audio.Controls);
            Assert.Contains("<audio controls>", audio.ToHtml());
        }

        [Fact]
        public void Constructor_WithAutoplayLoopMuted_ShouldAddAttributes()
        {
            // Act
            var audio = new HtmlAudio("audio.mp3", true, true, true, true);

            // Assert
            Assert.True(audio.Autoplay);
            Assert.True(audio.Loop);
            Assert.True(audio.Muted);
            Assert.Contains("autoplay", audio.ToHtml());
            Assert.Contains("loop", audio.ToHtml());
            Assert.Contains("muted", audio.ToHtml());
        }

        [Fact]
        public void Constructor_WithStyle_ShouldAddStyle()
        {
            // Act
            var audio = new HtmlAudio("audio.mp3", true, false, false, false, "width:100%;");

            // Assert
            Assert.Contains("style=\"width:100%;\"", audio.ToHtml());
        }

        [Fact]
        public void ToString_ShouldReturnHtml()
        {
            // Act
            var audio = new HtmlAudio("test.mp3");

            // Assert
            Assert.Equal(audio.ToHtml(), audio.ToString());
        }
    }
}