using Jasmine.Format.Elements.Media;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class MediaModule : ExampleModuleBase
    {
        public override string Name => "HtmlVideo & HtmlAudio";
        public override string Id => "media";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlVideo & HtmlAudio - 媒体元素");

            var video1 = new HtmlVideo(Constants.TestVideoUrl);
            var video2 = new HtmlVideo(Constants.TestVideoUrl, "640", "480");
            var video3 = new HtmlVideo(Constants.TestVideoUrl, "width:100%; max-width:500px;");
            
            var audio1 = new HtmlAudio(Constants.TestAudioUrl);
            var audio2 = new HtmlAudio(Constants.TestAudioUrl, style: "width:300px;");

            AddCard(htmlBuilder, "视频元素", @"new HtmlVideo(Constants.TestVideoUrl).ToHtml()", video1.ToHtml());
            AddCard(htmlBuilder, "视频(固定尺寸)", @"new HtmlVideo(Constants.TestVideoUrl, ""640"", ""480"").ToHtml()", video2.ToHtml());
            AddCard(htmlBuilder, "视频(自定义样式)", @"new HtmlVideo(Constants.TestVideoUrl, ""width:100%; max-width:500px;"").ToHtml()", video3.ToHtml());
            AddCard(htmlBuilder, "音频元素", @"new HtmlAudio(Constants.TestAudioUrl).ToHtml()", audio1.ToHtml());
            AddCard(htmlBuilder, "音频(自定义样式)", @"new HtmlAudio(Constants.TestAudioUrl, ""width:300px;"").ToHtml()", audio2.ToHtml());

            AddSectionEnd(htmlBuilder);
        }
    }
}
