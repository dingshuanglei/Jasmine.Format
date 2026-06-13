namespace Jasmine.Format.Utilities
{
    /// <summary>
    /// Base class for HTML elements that support CSS styling.
    /// </summary>
    public abstract class HtmlStyledElement : HtmlElementBase
    {
        /// <summary>
        /// Gets or sets the CSS style of the element.
        /// </summary>
        public string Style { get; protected set; }

        /// <summary>
        /// Gets the style attribute string for the element.
        /// </summary>
        /// <returns>The style attribute with proper HTML encoding, or empty string if no style is set.</returns>
        protected string GetStyleAttribute()
        {
            return !string.IsNullOrWhiteSpace(Style) ? $" style=\"{HtmlEncoder.HtmlEncode(Style)}\"" : "";
        }
    }
}