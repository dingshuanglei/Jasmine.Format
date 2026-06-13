using Jasmine.Format.Elements.List;
using Jasmine.Format.Elements.Text;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class ListModule : ExampleModuleBase
    {
        public override string Name => "HtmlList";
        public override string Id => "list";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlList - 列表元素");

            var ul1 = new HtmlList(ListType.Unordered).AddItem("项目一").AddItem("项目二").AddItem("项目三");
            var ol1 = new HtmlList(ListType.Ordered).AddItem("步骤一").AddItem("步骤二").AddItem("步骤三");
            var ol2 = new HtmlList(ListType.Ordered, 5).AddItem("第5步").AddItem("第6步").AddItem("第7步");
            var ul3 = new HtmlList("list-style-type:square;", ListType.Unordered).AddItem("方形项目一").AddItem("方形项目二");

            var items = new[] { "A项", "B项", "C项" };
            var li1 = new HtmlLi().AddSpanRange(items, x => x, "green");
            var li2 = new HtmlLi().AddSpanRange(items, x => $"[{x}]", "#0066cc", "font-weight:bold;");
            var ul4 = new HtmlList(ListType.Unordered).AddItem(li1).AddItem(li2);

            var li3 = new HtmlLi("普通列表项");
            var li4 = new HtmlLi("<b>粗体</b>", false);
            var li5 = new HtmlLi().Add(new HtmlSpan("带样式", "#ff6600"));

            var ul5 = new HtmlList(ListType.Unordered).AddRange(items);
            var trains = new[] { new { Station = "北京", Train = "G123" }, new { Station = "上海", Train = "G456" } };
            var ul6 = new HtmlList(ListType.Unordered).AddRange(trains.Select(t => $"{t.Station} - {t.Train}"));

            AddCard(htmlBuilder, "无序列表", @"new HtmlList(ListType.Unordered).AddItem(""项目一"").AddItem(""项目二"").AddItem(""项目三"").ToHtml()", ul1.ToHtml());
            AddCard(htmlBuilder, "有序列表", @"new HtmlList(ListType.Ordered).AddItem(""步骤一"").AddItem(""步骤二"").AddItem(""步骤三"").ToHtml()", ol1.ToHtml());
            AddCard(htmlBuilder, "有序列表(起始值)", @"new HtmlList(ListType.Ordered, 5).AddItem(""第5步"").AddItem(""第6步"").AddItem(""第7步"").ToHtml()", ol2.ToHtml());
            AddCard(htmlBuilder, "带样式的无序列表", @"new HtmlList(""list-style-type:square;"", ListType.Unordered).AddItem(""方形项目一"").AddItem(""方形项目二"").ToHtml()", ul3.ToHtml());
            AddCard(htmlBuilder, "HtmlLi 基础", @"new HtmlLi(""普通列表项"").ToHtml()", li3.ToHtml());
            AddCard(htmlBuilder, "HtmlLi 不编码", @"new HtmlLi(""<b>粗体</b>"", false).ToHtml()", li4.ToHtml());
            AddCard(htmlBuilder, "HtmlLi + HtmlSpan", @"new HtmlLi().Add(new HtmlSpan(""带样式"", ""#ff6600"")).ToHtml()", li5.ToHtml());
            AddCard(htmlBuilder, "HtmlLi + AddSpanRange", @"new HtmlLi().AddSpanRange(items, x => x, ""green"").ToHtml()", li1.ToHtml());
            AddCard(htmlBuilder, "批量添加列表项", @"new HtmlList(ListType.Unordered).AddRange(items).ToHtml()", ul5.ToHtml());
            AddCard(htmlBuilder, "批量添加(选择器)", @"new HtmlList(ListType.Unordered).AddRange(trains.Select(t => $""{t.Station} - {t.Train}"")).ToHtml()", ul6.ToHtml());
            AddCard(htmlBuilder, "列表嵌套SpanRange", @"new HtmlList(ListType.Unordered).AddItem(li1).AddItem(li2).ToHtml()", ul4.ToHtml());

            AddSectionEnd(htmlBuilder);
        }
    }
}