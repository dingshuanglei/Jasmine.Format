using Jasmine.Format.Utilities;
using System.Text;

namespace Jasmine.Format.Elements.Basic
{
    /// <summary>
    /// Represents an HTML anchor (link) element.
    /// </summary>
    public class HtmlA : HtmlStyledElement
    {
        /// <summary>
        /// Gets the content text of the anchor.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the URL the anchor points to.
        /// </summary>
        public string Href { get; }

        /// <summary>
        /// Gets the target attribute value (e.g., "_blank", "_self").
        /// </summary>
        public string Target { get; }

        /// <summary>
        /// Gets the text color of the anchor.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlA"/> class.
        /// </summary>
        /// <param name="content">The text content of the link.</param>
        /// <param name="href">The URL the link points to.</param>
        /// <param name="target">The target window/frame for the link. Default is null.</param>
        /// <param name="color">The text color of the link. Default is null.</param>
        /// <param name="style">Optional CSS style for the element. Default is null.</param>
        public HtmlA(string content, string href, string target = null, string color = null, string style = null)
        {
            Content = content ?? string.Empty;
            Href = href ?? string.Empty;
            Target = target;
            Color = color;
            Style = style;
        }

        /// <summary>
        /// Creates a new HtmlA with the specified target.
        /// </summary>
        /// <param name="target">The target window/frame for the link.</param>
        /// <returns>A new HtmlA instance with the specified target.</returns>
        public HtmlA WithTarget(string target)
        {
            return new HtmlA(Content, Href, target, Color, Style);
        }

        /// <summary>
        /// Creates a new HtmlA with the specified color.
        /// </summary>
        /// <param name="color">The text color of the link.</param>
        /// <returns>A new HtmlA instance with the specified color.</returns>
        public HtmlA WithColor(string color)
        {
            return new HtmlA(Content, Href, Target, color, Style);
        }

        /// <summary>
        /// Creates a new HtmlA with the specified style.
        /// </summary>
        /// <param name="style">The CSS style to apply.</param>
        /// <returns>A new HtmlA instance with the specified style.</returns>
        public HtmlA WithStyle(string style)
        {
            return new HtmlA(Content, Href, Target, Color, style);
        }

        /// <summary>
        /// Converts the anchor element to an HTML string.
        /// </summary>
        /// <param name="escape">Whether to HTML encode the content. Default is true.</param>
        /// <returns>HTML string representation of the anchor element.</returns>
        public string ToHtml(bool escape = true)
        {
            string content = escape ? HtmlEncoder.HtmlEncode(Content) : Content;
            string href = HtmlEncoder.HtmlEncode(Href);
            string target = escape ? HtmlEncoder.HtmlEncode(Target) : Target;

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
            string targetAttr = !string.IsNullOrWhiteSpace(Target) ? $" target=\"{target}\"" : "";
            return $"<{HtmlTagNames.A} href=\"{href}\"{targetAttr}{styleAttr}>{content}</{HtmlTagNames.A}>";
        }

        /// <summary>
        /// Converts the anchor element to an HTML string with HTML encoding.
        /// </summary>
        /// <returns>HTML string representation of the anchor element.</returns>
        public override string ToHtml()
        {
            return ToHtml(true);
        }
    }
}