using Jasmine.Format.Elements.Table;
using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public class TableModule : ExampleModuleBase
    {
        public override string Name => "HtmlTable";
        public override string Id => "table";

        public override void Render(StringBuilder htmlBuilder)
        {
            AddSectionStart(htmlBuilder, "HtmlTable - 表格元素");

            var table1 = new HtmlTable()
                .AddHeader("姓名", "年龄", "职业")
                .AddRow("张三", "25", "工程师")
                .AddRow("李四", "30", "设计师");

            var thColspan = new HtmlCell("合并表头", CellType.Header, colSpan: 2);
            var tdColspan = new HtmlCell("跨列内容", CellType.Data, color: "#0066cc", colSpan: 3);
            var thRowspan = new HtmlCell("跨行表头", CellType.Header, color: "#009900", rowSpan: 3);
            var tdRowspan = new HtmlCell("跨行内容", CellType.Data, color: "#ff6600", style: "font-weight:bold;", rowSpan: 2);

            var rowWithColspan = new HtmlTr()
                .AddCell(new HtmlCell("普通单元格", CellType.Data, color: "#333"))
                .AddCell(new HtmlCell("跨2列", CellType.Data, color: "#0066cc", style: "text-align:center; font-weight:bold;", colSpan: 2));

            AddCard(htmlBuilder, "基础表格", @"new HtmlTable().AddHeader(""姓名"", ""年龄"", ""职业"").AddRow(""张三"", ""25"", ""工程师"").AddRow(""李四"", ""30"", ""设计师"").ToHtml()", table1.ToHtml());
            AddCard(htmlBuilder, "跨列表头 (colspan)", @"new HtmlCell(""合并表头"", CellType.Header, colSpan: 2)", thColspan.ToHtml());
            AddCard(htmlBuilder, "跨列单元格 (colspan)", @"new HtmlCell(""跨列内容"", CellType.Data, color: ""#0066cc"", colSpan: 3)", tdColspan.ToHtml());
            AddCard(htmlBuilder, "跨行表头 (rowspan)", @"new HtmlCell(""跨行表头"", CellType.Header, color: ""#009900"", rowSpan: 3)", thRowspan.ToHtml());
            AddCard(htmlBuilder, "跨行单元格 (rowspan)", @"new HtmlCell(""跨行内容"", CellType.Data, color: ""#ff6600"", style: ""font-weight:bold;"", rowSpan: 2)", tdRowspan.ToHtml());
            AddCard(htmlBuilder, "行内使用跨列", @"new HtmlTr().AddCell(""普通单元格"").AddCell(new HtmlCell(""跨2列"", CellType.Data, color: ""#0066cc"", colSpan: 2))", rowWithColspan.ToHtml());

            htmlBuilder.Append(@"
    <div class='card'>
        <h4>综合示例：带合并单元格的表格</h4>
        <div class='code-block'>// HtmlCell(content, CellType, color, style, rowSpan, colSpan)<br/>new HtmlCell(""部门"", CellType.Header, color: ""#333"", style: ""background:#f0f0f0;"", rowSpan: 2)</div>
        <div style='overflow-x:auto;'>
            <table style='border-collapse:collapse; width:100%; border:1px solid #ccc;'>
                <thead>
                    <tr>
                        <th rowspan='2' style='background:#f0f0f0; padding:8px; border:1px solid #ccc;'>部门</th>
                        <th style='background:#f0f0f0; padding:8px; border:1px solid #ccc;'>姓名</th>
                        <th colspan='2' style='background:#f0f0f0; padding:8px; border:1px solid #ccc;'>职位</th>
                    </tr>
                    <tr>
                        <th style='background:#f0f0f0; padding:8px; border:1px solid #ccc;'>姓名</th>
                        <th style='background:#f0f0f0; padding:8px; border:1px solid #ccc;'>级别</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style='border:1px solid #ccc; padding:8px;'>张三</td>
                        <td style='border:1px solid #ccc; padding:8px;'>前端工程师</td>
                        <td style='border:1px solid #ccc; padding:8px;'>高级</td>
                    </tr>
                    <tr>
                        <td style='border:1px solid #ccc; padding:8px;'>李四</td>
                        <td style='border:1px solid #ccc; padding:8px;'>后端工程师</td>
                        <td style='border:1px solid #ccc; padding:8px;'>中级</td>
                    </tr>
                    <tr>
                        <td colspan='3' style='border:1px solid #ccc; padding:8px; text-align:center; font-weight:bold; color:#0066cc;'>技术部小计</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>");

            AddSectionEnd(htmlBuilder);
        }
    }
}