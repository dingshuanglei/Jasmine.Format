using Jasmine.Format.Utilities;
using System.Text;

namespace Jasmine.Format.Elements.Text
{
    /// <summary>
    /// Represents an HTML span element.
    /// </summary>
    public class HtmlSpan : HtmlStyledElement
    {
        /// <summary>
        /// Gets the content text of the span.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the text color of the span.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlSpan"/> class.
        /// </summary>
        /// <param name="content">The text content of the span.</param>
        /// <param name="color">The text color of the span. Default is null.</param>
        /// <param name="style">Optional CSS style for the element. Default is null.</param>
        public HtmlSpan(string content, string color = null, string style = null)
        {
            Content = content ?? string.Empty;
            Color = color;
            Style = style;
        }

        /// <summary>
        /// Creates a new HtmlSpan with the specified color.
        /// </summary>
        /// <param name="color">The text color of the span.</param>
        /// <returns>A new HtmlSpan instance with the specified color.</returns>
        public HtmlSpan WithColor(string color)
        {
            return new HtmlSpan(Content, color, Style);
        }

        /// <summary>
        /// Creates a new HtmlSpan with the specified style.
        /// </summary>
        /// <param name="style">The CSS style to apply.</param>
        /// <returns>A new HtmlSpan instance with the specified style.</returns>
        public HtmlSpan WithStyle(string style)
        {
            return new HtmlSpan(Content, Color, style);
        }

        /// <summary>
        /// Converts the span element to an HTML string.
        /// </summary>
        /// <param name="escape">Whether to HTML encode the content. Default is true.</param>
        /// <returns>HTML string representation of the span element.</returns>
        public string ToHtml(bool escape = true)
        {
            string content = escape ? HtmlEncoder.HtmlEncode(Content) : Content;

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
            return $"<{HtmlTagNames.Span}{styleAttr}>{content}</{HtmlTagNames.Span}>";
        }

        /// <summary>
        /// Converts the span element to an HTML string with HTML encoding.
        /// </summary>
        /// <returns>HTML string representation of the span element.</returns>
        public override string ToHtml()
        {
            return ToHtml(true);
        }
    }
}