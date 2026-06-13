using Jasmine.Format.Elements.Text;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class SpanModule : ExampleModuleBase
    {
        public override string Name => "HtmlSpan";
        public override string Id => "span";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlSpan - Span 元素构建");

            var span1 = new HtmlSpan("普通文本");
            var span2 = new HtmlSpan("红色文本", "#ff0000");
            var span3 = new HtmlSpan("粗体文本", "#333", "font-weight:bold;");
            var span4 = new HtmlSpan("原始文本").WithColor("blue");
            var span5 = new HtmlSpan("文本").WithStyle("font-size:18px;color:#666;");
            var span6 = new HtmlSpan("<b>不编码</b>", "#ff0000").WithStyle("font-size:16px;");

            AddCard(htmlBuilder, "基础用法", @"new HtmlSpan(""普通文本"").ToHtml()", span1.ToHtml());
            AddCard(htmlBuilder, "带颜色", @"new HtmlSpan(""红色文本"", ""#ff0000"").ToHtml()", span2.ToHtml());
            AddCard(htmlBuilder, "带颜色和样式", @"new HtmlSpan(""粗体文本"", ""#333"", ""font-weight:bold;""),ToHtml()", span3.ToHtml());
            AddCard(htmlBuilder, "WithColor 方法", @"new HtmlSpan(""原始文本"").WithColor(""blue"").ToHtml()", span4.ToHtml());
            AddCard(htmlBuilder, "WithStyle 方法", @"new HtmlSpan(""文本"").WithStyle(""font-size:18px;color:#666;"").ToHtml()", span5.ToHtml());
            AddCard(htmlBuilder, "链式调用", @"new HtmlSpan(""<b>不编码</b>"", ""#ff0000"").WithStyle(""font-size:16px;""),ToHtml()", span6.ToHtml());

            AddSectionEnd(htmlBuilder);
        }
    }
}
