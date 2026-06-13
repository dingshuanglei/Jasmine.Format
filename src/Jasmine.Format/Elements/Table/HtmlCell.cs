using Jasmine.Format.Utilities;
using System.Text;

namespace Jasmine.Format.Elements.Table
{
    /// <summary>
    /// Specifies the type of HTML table cell.
    /// </summary>
    public enum CellType
    {
        /// <summary>
        /// Header cell (th element).
        /// </summary>
        Header,
        /// <summary>
        /// Data cell (td element).
        /// </summary>
        Data
    }

    /// <summary>
    /// Represents an HTML table cell element.
    /// </summary>
    public sealed class HtmlCell : HtmlStyledElement
    {
        /// <summary>
        /// Gets the content of the cell.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the text color of the cell content.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// Gets the number of rows this cell spans.
        /// </summary>
        public int RowSpan { get; }

        /// <summary>
        /// Gets the number of columns this cell spans.
        /// </summary>
        public int ColSpan { get; }

        /// <summary>
        /// Gets the type of the cell (Header or Data).
        /// </summary>
        public CellType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlCell"/> class.
        /// </summary>
        /// <param name="content">The content of the cell (required).</param>
        /// <param name="type">The type of the cell. Default is Data.</param>
        /// <param name="color">The text color of the cell content. Default is null.</param>
        /// <param name="style">Optional CSS style for the element. Default is null.</param>
        /// <param name="rowSpan">The number of rows this cell spans. Default is 0.</param>
        /// <param name="colSpan">The number of columns this cell spans. Default is 0.</param>
        public HtmlCell(string content, CellType type = CellType.Data, string color = null,
                        string style = null, int rowSpan = 0, int colSpan = 0)
        {
            Content = content ?? string.Empty;
            Type = type;
            Color = color;
            Style = style;
            RowSpan = rowSpan > 0 ? rowSpan : 0;
            ColSpan = colSpan > 0 ? colSpan : 0;
        }

        /// <summary>
        /// Converts the cell element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the cell element.</returns>
        public override string ToHtml()
        {
            string encodedContent = HtmlEncoder.HtmlEncode(Content);
            string tag = Type == CellType.Header ? HtmlTagNames.Th : HtmlTagNames.Td;

            StringBuilder styleBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Color))
            {
                styleBuilder.Append($"color:{HtmlEncoder.HtmlEncode(Color)};");
            }
            if (!string.IsNullOrWhiteSpace(Style))
            {
                styleBuilder.Append(HtmlEncoder.HtmlEncode(Style));
            }

            string styleAttr = styleBuilder.Length > 0 ? $" style=\"{styleBuilder}\"" : "";
            string rowSpanAttr = RowSpan > 1 ? $" rowspan=\"{RowSpan}\"" : "";
            string colSpanAttr = ColSpan > 1 ? $" colspan=\"{ColSpan}\"" : "";

            return $"<{tag}{rowSpanAttr}{colSpanAttr}{styleAttr}>{encodedContent}</{tag}>";
        }
    }
}