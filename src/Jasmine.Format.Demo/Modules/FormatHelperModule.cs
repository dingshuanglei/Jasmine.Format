using Jasmine.Format.Utilities;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class FormatHelperModule : ExampleModuleBase
    {
        public override string Name => "HtmlFormatHelper";
        public override string Id => "format-helper";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlFormatHelper - HTML 模板格式化");

            var result1 = HtmlFormatHelper.Format("欢迎: {0}, 年龄: {1}", "张三", "25");
            var result2 = HtmlFormatHelper.Format("<p>用户: {0}</p>", "<script>alert('XSS')</script>");
            
            var kvPairs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("name", "李四"),
                new KeyValuePair<string, string>("age", "30")
            };
            var result3 = HtmlFormatHelper.FormatTemplate("姓名: {name}, 年龄: {age}", kvPairs);

            var dict = new Dictionary<string, string>
            {
                { "title", "标题" },
                { "content", "内容" }
            };
            var result4 = HtmlFormatHelper.FormatTemplate("<div><h3>{title}</h3><p>{content}</p></div>", dict);

            var result5 = HtmlFormatHelper.FormatTemplate("<p>{html}</p>", false, 
                new KeyValuePair<string, string>("html", "<b>粗体文本</b>"));

            AddCard(htmlBuilder, "Format - 位置参数", @"HtmlFormatHelper.Format(""欢迎: {0}, 年龄: {1}"", ""张三"", ""25"")", result1);
            AddCard(htmlBuilder, "Format - 自动编码", @"HtmlFormatHelper.Format(""<p>用户: {0}</p>"", ""<script>alert('XSS')</script>"")", result2);
            AddCard(htmlBuilder, "FormatTemplate - KeyValuePair", @"HtmlFormatHelper.FormatTemplate(""姓名: {name}, 年龄: {age}"", kvPairs)", result3);
            AddCard(htmlBuilder, "FormatTemplate - Dictionary", @"HtmlFormatHelper.FormatTemplate(""<div><h3>{title}</h3><p>{content}</p></div>"", dict)", result4);
            AddCard(htmlBuilder, "FormatTemplate - 禁用编码", @"HtmlFormatHelper.FormatTemplate(""<p>{html}</p>"", false, ...)", result5);

            AddSectionEnd(htmlBuilder);
        }
    }
}
