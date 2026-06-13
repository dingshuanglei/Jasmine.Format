using Jasmine.Format.Utilities;
using System.Text;

namespace Jasmine.Format.Elements.Basic
{
    /// <summary>
    /// Represents an HTML image element.
    /// </summary>
    public class HtmlImg : HtmlStyledElement
    {
        /// <summary>
        /// Gets the source URL of the image.
        /// </summary>
        public string Src { get; }

        /// <summary>
        /// Gets the alternate text for the image.
        /// </summary>
        public string Alt { get; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public string Width { get; }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public string Height { get; }

        /// <summary>
        /// Gets the CSS class of the image.
        /// </summary>
        public string Class { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlImg"/> class.
        /// </summary>
        /// <param name="src">The source URL of the image.</param>
        /// <param name="alt">The alternate text for the image.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="style">Optional CSS style for the element.</param>
        /// <param name="class">Optional CSS class for the element.</param>
        public HtmlImg(string src, string alt = null, string width = null, string height = null, string style = null, string @class = null)
        {
            Src = src ?? string.Empty;
            Alt = alt ?? string.Empty;
            Width = width;
            Height = height;
            Style = style;
            Class = @class;
        }

        /// <summary>
        /// Converts the image element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the image element.</returns>
        public override string ToHtml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"<{HtmlTagNames.Img} src=\"{HtmlEncoder.HtmlEncode(Src)}\" alt=\"{HtmlEncoder.HtmlEncode(Alt)}\"");

            if (!string.IsNullOrWhiteSpace(Width))
                builder.Append($" width=\"{HtmlEncoder.HtmlEncode(Width)}\"");
            if (!string.IsNullOrWhiteSpace(Height))
                builder.Append($" height=\"{HtmlEncoder.HtmlEncode(Height)}\"");
            if (!string.IsNullOrWhiteSpace(Class))
                builder.Append($" class=\"{HtmlEncoder.HtmlEncode(Class)}\"");
            if (!string.IsNullOrWhiteSpace(Style))
                builder.Append($" style=\"{HtmlEncoder.HtmlEncode(Style)}\"");

            builder.Append(" />");
            return builder.ToString();
        }
    }
}