using Jasmine.Format.Elements.Basic;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class ImgModule : ExampleModuleBase
    {
        public override string Name => "HtmlImg";
        public override string Id => "img";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlImg - 图片元素");

            var img1 = new HtmlImg(Constants.TestImageUrl, "示例图片");
            var img2 = new HtmlImg(Constants.TestImageUrl, "固定尺寸", "150", "100");
            var img3 = new HtmlImg(Constants.TestImageUrl, "带边框", "100", "100", "border:2px solid #ccc; border-radius:4px;");

            AddCard(htmlBuilder, "基础图片", @"new HtmlImg(Constants.TestImageUrl, ""示例图片"").ToHtml()", img1.ToHtml());
            AddCard(htmlBuilder, "固定尺寸", @"new HtmlImg(Constants.TestImageUrl, ""固定尺寸"", ""150"", ""100"").ToHtml()", img2.ToHtml());
            AddCard(htmlBuilder, "带样式", @"new HtmlImg(Constants.TestImageUrl, ""带边框"", ""100"", ""100"", ""border:2px solid #ccc; border-radius:4px;"").ToHtml()", img3.ToHtml());

            AddSectionEnd(htmlBuilder);
        }
    }
}
