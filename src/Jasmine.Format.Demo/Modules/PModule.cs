using Jasmine.Format.Elements.Basic;
using Jasmine.Format.Elements.Text;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class PModule : ExampleModuleBase
    {
        public override string Name => "HtmlP";
        public override string Id => "p";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlP - 段落构建器");

            var p1 = new HtmlP().Add("普通段落文本").WithStyle("text-indent: 28px;font-weight: 600;font-size:15px;margin-bottom: 0;");
            var p2 = new HtmlP().Add("您好，").Add(new HtmlSpan("张三", "#0066cc")).Add("，欢迎使用！");
            var p3 = new HtmlP().Add("访问 ").Add(new HtmlA("示例网站", Constants.TestDomainUrl)).Add(" 了解更多");
            var p4 = new HtmlP().Add("图片：").Add(new HtmlImg(Constants.TestImageUrl, "示例图片"));
            var p5 = new HtmlP().Add("第一行").AddRaw("<br/>").Add("第二行");

            var trains = new[] {
                new { Station = "北京", TrainNo = "G123", Status = "正常" },
                new { Station = "上海", TrainNo = "G456", Status = "晚点" },
                new { Station = "广州", TrainNo = "G789", Status = "正常" }
            };
            var p6 = new HtmlP().Add("列车信息：").AddSpanRange(trains, t => $"{t.Station}开{t.TrainNo}次{t.Status}", "blue").WithStyle("text-indent: 28px;font-weight: 600;font-size:15px;margin-bottom: 0;");
            var p7 = new HtmlP().AddSpanRange(trains, t => t.Station, "#ff6600", "font-weight:bold;").WithStyle("text-indent: 28px;font-weight: 600;font-size:15px;margin-bottom: 0;");

            var p8 = new HtmlP().Add("列车详情：").AddSpanRange(trains, t => new HtmlSpan($"{t.Station}开{t.TrainNo}", "red")).WithStyle("text-indent: 28px;font-weight: 600;font-size:15px;margin-bottom: 0;");
            var p9 = new HtmlP().AddSpanRange(trains, t => new HtmlSpan(t.Status, t.Status == "正常" ? "green" : "red", "font-weight:bold;")).WithStyle("text-indent: 28px;font-weight: 600;font-size:15px;margin-bottom: 0;");
            var p10 = new HtmlP().Add("列车详情：").AddRawRange(trains, t => $"{new HtmlSpan(t.Station, "red")}开{new HtmlSpan(t.TrainNo, "blue")}次{new HtmlSpan(t.Status, t.Status == "正常" ? "green" : "red")}").WithStyle("text-indent: 28px;font-weight: 600;font-size:15px;margin-bottom: 0;");

            // ToPlainText 示例 - 复杂内容对比
            var complexP = new HtmlP()
                .Add("用户：")
                .Add(new HtmlSpan("张三", "#0066cc", "font-weight:bold;"))
                .Add(" - ")
                .Add(new HtmlA("查看资料", Constants.TestDomainUrl, "_blank"))
                .Add(" - ")
                .Add(new HtmlImg(Constants.TestImageUrl, "头像"));
            var plainText = complexP.ToPlainText();

            AddCard(htmlBuilder, "Add - 添加文本", @"new HtmlP().Add(""普通段落文本"").ToHtml()", p1.ToHtml());
            AddCard(htmlBuilder, "Add + HtmlSpan - 链式调用", @"new HtmlP().Add(""您好，"").Add(new HtmlSpan(""张三"", ""#0066cc"")).Add(""，欢迎使用！"").ToHtml()", p2.ToHtml());
            AddCard(htmlBuilder, "Add + HtmlA - 添加链接", @"new HtmlP().Add(""访问 "").Add(new HtmlA(""示例网站"", Constants.TestDomainUrl)).Add("" 了解更多"").ToHtml()", p3.ToHtml());
            AddCard(htmlBuilder, "Add + HtmlImg - 添加图片", @"new HtmlP().Add(""图片："").Add(new HtmlImg(Constants.TestImageUrl, ""示例图片"")).ToHtml()", p4.ToHtml());
            AddCard(htmlBuilder, "AddRaw - 添加换行", @"new HtmlP().Add(""第一行"").AddRaw(""<br/>"").Add(""第二行"").ToHtml()", p5.ToHtml());
            AddCard(htmlBuilder, "AddSpanRange - 批量添加Span", @"new HtmlP().Add(""列车信息："").AddSpanRange(trains, t => $""{t.Station}开{t.TrainNo}次{t.Status}"", ""blue"").ToHtml()", p6.ToHtml());
            AddCard(htmlBuilder, "AddSpanRange - 带样式", @"new HtmlP().AddSpanRange(trains, t => t.Station, ""#ff6600"", ""font-weight:bold;""),ToHtml()", p7.ToHtml());
            AddCard(htmlBuilder, "AddSpanRange - 返回HtmlSpan", @"new HtmlP().Add(""列车详情："").AddSpanRange(trains, t => new HtmlSpan($""{t.Station}开{t.TrainNo}"", ""red""))", p8.ToHtml());
            AddCard(htmlBuilder, "AddSpanRange - 动态样式", @"new HtmlP().AddSpanRange(trains, t => new HtmlSpan(t.Status, t.Status == ""正常"" ? ""green"" : ""red"", ""font-weight:bold;""))", p9.ToHtml());
            AddCard(htmlBuilder, "AddRawRange - 多个Span组合", @"new HtmlP().Add(""列车详情："").AddRawRange(trains, t => $""{new HtmlSpan(t.Station, ""red"")}开{new HtmlSpan(t.TrainNo, ""blue"")}次{new HtmlSpan(t.Status, t.Status == ""正常"" ? ""green"" : ""red"")}"")", p10.ToHtml());
            AddCard(htmlBuilder, "ToPlainText - 提取纯文本 (HTML)", @"complexP.ToHtml()", complexP.ToHtml());
            AddCard(htmlBuilder, "ToPlainText - 提取纯文本 (Text)", @"complexP.ToPlainText()", $"<span>{plainText}</span>");

            AddSectionEnd(htmlBuilder);
        }
    }
}