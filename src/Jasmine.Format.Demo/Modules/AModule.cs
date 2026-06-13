using Jasmine.Format.Elements.Basic;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class AModule : ExampleModuleBase
    {
        public override string Name => "HtmlA";
        public override string Id => "a";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlA - 超链接元素");

            var link1 = new HtmlA("访问网站", Constants.TestDomainUrl);
            var link2 = new HtmlA("新窗口打开", Constants.TestDomainUrl, "_blank");
            var link3 = new HtmlA("红色链接", Constants.TestDomainUrl, "_blank", "#ff0000");

            AddCard(htmlBuilder, "基础链接", @"new HtmlA(""访问网站"", Constants.TestDomainUrl).ToHtml()", link1.ToHtml());
            AddCard(htmlBuilder, "新窗口打开", @"new HtmlA(""新窗口打开"", Constants.TestDomainUrl, ""_blank"").ToHtml()", link2.ToHtml());
            AddCard(htmlBuilder, "带颜色", @"new HtmlA(""红色链接"", Constants.TestDomainUrl, ""_blank"", ""red"").ToHtml()", link3.ToHtml());

            AddSectionEnd(htmlBuilder);
        }
    }
}
