using Jasmine.Format.Elements.Table;

namespace Jasmine.Format.Tests
{
    public class HtmlTableTests
    {
        [Fact]
        public void Constructor_ShouldCreateEmptyTable()
        {
            // Act
            var table = new HtmlTable();

            // Assert
            Assert.Equal("<table></table>", table.ToHtml());
        }

        [Fact]
        public void WithStyle_ShouldSetStyle()
        {
            // Act
            var table = new HtmlTable().WithStyle("border-collapse:collapse;");

            // Assert
            Assert.Contains("style=\"border-collapse:collapse;\"", table.ToHtml());
        }

        [Fact]
        public void AddHeader_ShouldAddHeaderRow()
        {
            // Act
            var table = new HtmlTable().AddHeader("姓名", "年龄", "职业");

            // Assert
            Assert.Contains("<thead>", table.ToHtml());
            Assert.Contains("<th>姓名</th>", table.ToHtml());
            Assert.Contains("<th>年龄</th>", table.ToHtml());
            Assert.Contains("<th>职业</th>", table.ToHtml());
        }

        [Fact]
        public void AddRow_ShouldAddDataRow()
        {
            // Act
            var table = new HtmlTable().AddRow("张三", "25", "工程师");

            // Assert
            Assert.Contains("<tbody>", table.ToHtml());
            Assert.Contains("<td>张三</td>", table.ToHtml());
            Assert.Contains("<td>25</td>", table.ToHtml());
            Assert.Contains("<td>工程师</td>", table.ToHtml());
        }

        [Fact]
        public void AddHeaderAndRows_ShouldCreateFullTable()
        {
            // Act
            var table = new HtmlTable()
                .AddHeader("姓名", "年龄")
                .AddRow("张三", "25")
                .AddRow("李四", "30");

            // Assert
            Assert.Contains("<thead>", table.ToHtml());
            Assert.Contains("<tbody>", table.ToHtml());
            Assert.Equal(2, table.Headers.Count);
            Assert.Equal(3, table.Rows.Count);
        }

        [Fact]
        public void ToString_ShouldReturnHtml()
        {
            // Act
            var table = new HtmlTable().AddHeader("列1");

            // Assert
            Assert.Equal(table.ToHtml(), table.ToString());
        }

        [Fact]
        public void HtmlCell_WithColSpan_ShouldRenderColSpanAttribute()
        {
            // Act
            var th = new HtmlCell("合并表头", CellType.Header, colSpan: 2);

            // Assert
            Assert.Equal("<th colspan=\"2\">合并表头</th>", th.ToHtml());
        }

        [Fact]
        public void HtmlCell_WithRowSpan_ShouldRenderRowSpanAttribute()
        {
            // Act
            var th = new HtmlCell("跨行表头", CellType.Header, color: "red", rowSpan: 3);

            // Assert
            Assert.Equal("<th rowspan=\"3\" style=\"color:red;\">跨行表头</th>", th.ToHtml());
        }

        [Fact]
        public void HtmlCell_DataWithColSpan_ShouldRenderColSpanAttribute()
        {
            // Act
            var td = new HtmlCell("合并单元格", CellType.Data, colSpan: 3);

            // Assert
            Assert.Equal("<td colspan=\"3\">合并单元格</td>", td.ToHtml());
        }

        [Fact]
        public void HtmlCell_DataWithRowSpan_ShouldRenderRowSpanAttribute()
        {
            // Act
            var td = new HtmlCell("跨行单元格", CellType.Data, color: "blue", style: "font-weight:bold;", rowSpan: 2);

            // Assert
            Assert.Equal("<td rowspan=\"2\" style=\"color:blue;font-weight:bold;\">跨行单元格</td>", td.ToHtml());
        }

        [Fact]
        public void HtmlCell_WithBothSpans_ShouldRenderBothAttributes()
        {
            // Act
            var td = new HtmlCell("跨行跨列", CellType.Data, color: "green", rowSpan: 2, colSpan: 3);

            // Assert
            Assert.Equal("<td rowspan=\"2\" colspan=\"3\" style=\"color:green;\">跨行跨列</td>", td.ToHtml());
        }
    }
}