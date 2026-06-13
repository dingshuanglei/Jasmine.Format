using Jasmine.Format.Elements.Container;
using Jasmine.Format.Elements.Text;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class DivModule : ExampleModuleBase
    {
        public override string Name => "HtmlDiv";
        public override string Id => "div";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlDiv - 容器元素");

            var div1 = new HtmlDiv();
            var div2 = new HtmlDiv("border:1px solid #ccc; padding:10px; border-radius:4px;");
            var div3 = new HtmlDiv("background:#f5f5f5; padding:15px;").Add(new HtmlP().Add("这是一个段落"));
            
            var div4 = new HtmlDiv("border:1px solid #0066cc; padding:20px;")
                .Add(new HtmlP().Add("标题"))
                .Add(new HtmlP().Add(new HtmlSpan("高亮文本", "#ff6600")));

            var items = new[] { "项目A", "项目B", "项目C" };
            var div5 = new HtmlDiv("background:#fff;");
            foreach (var item in items)
            {
                div5 = div5.Add(new HtmlP().Add(new HtmlSpan(item, "#333")));
            }

            AddCard(htmlBuilder, "空容器", @"new HtmlDiv().ToHtml()", div1.ToHtml());
            AddCard(htmlBuilder, "带样式的容器", @"new HtmlDiv(""border:1px solid #ccc; padding:10px; border-radius:4px;"").ToHtml()", div2.ToHtml());
            AddCard(htmlBuilder, "包含段落", @"new HtmlDiv(""background:#f5f5f5; padding:15px;"").Add(new HtmlP().Add(""这是一个段落"")).ToHtml()", div3.ToHtml());
            AddCard(htmlBuilder, "多个子元素", @"new HtmlDiv(""border:1px solid #0066cc; padding:20px;"").Add(new HtmlP().Add(""标题"")).Add(new HtmlP().Add(new HtmlSpan(""高亮文本"", ""#ff6600""))).ToHtml()", div4.ToHtml());
            AddCard(htmlBuilder, "循环添加", @"foreach(var item in items) { div = div.Add(new HtmlP().Add(new HtmlSpan(item, ""#333""))); }", div5.ToHtml());

            AddSectionEnd(htmlBuilder);
        }
    }
}
