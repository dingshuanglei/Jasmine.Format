using Jasmine.Format.Elements.Basic;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class HtmlAModule : ExampleModuleBase
    {
        public override string Name => "HtmlA";
        public override string Id => "a";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlA - 链接元素");

            var link1 = new HtmlA("访问示例网站", Constants.TestDomainUrl);
            var link2 = new HtmlA("新窗口打开", Constants.TestDomainUrl, "_blank");
            var link3 = new HtmlA("红色链接", Constants.TestDomainUrl, "_blank", "#ff0000");
            var link4 = new HtmlA("无下划线", Constants.TestDomainUrl, "_blank", "#0066cc", "text-decoration:none;");
            var link5 = new HtmlA("链接文本", Constants.TestDomainUrl).WithColor("green");
            var link6 = new HtmlA("链接文本", Constants.TestDomainUrl).WithTarget("_self").WithStyle("font-weight:bold;");

            AddCard(htmlBuilder, "基础链接", @"new HtmlA(""访问示例网站"", Constants.TestDomainUrl).ToHtml()", link1.ToHtml());
            AddCard(htmlBuilder, "新窗口打开", @"new HtmlA(""新窗口打开"", Constants.TestDomainUrl, ""_blank"").ToHtml()", link2.ToHtml());
            AddCard(htmlBuilder, "带颜色", @"new HtmlA(""红色链接"", Constants.TestDomainUrl, ""_blank"", ""#ff0000"").ToHtml()", link3.ToHtml());
            AddCard(htmlBuilder, "带样式", @"new HtmlA(""无下划线"", Constants.TestDomainUrl, ""_blank"", ""#0066cc"", ""text-decoration:none;""),ToHtml()", link4.ToHtml());
            AddCard(htmlBuilder, "WithColor 方法", @"new HtmlA(""链接文本"", Constants.TestDomainUrl).WithColor(""green"").ToHtml()", link5.ToHtml());
            AddCard(htmlBuilder, "链式调用", @"new HtmlA(""链接文本"", Constants.TestDomainUrl).WithTarget(""_self"").WithStyle(""font-weight:bold;""),ToHtml()", link6.ToHtml());

            AddSectionEnd(htmlBuilder);
        }
    }
}