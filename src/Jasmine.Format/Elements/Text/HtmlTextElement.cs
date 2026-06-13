namespace Jasmine.Format.Elements.Text
{
    /// <summary>
    /// HTML text element type enumeration.
    /// </summary>
    public enum HtmlTextElementType
    {
        /// <summary>
        /// Normal text (requires HTML encoding).
        /// </summary>
        Text,
        /// <summary>
        /// Raw HTML (no encoding needed).
        /// </summary>
        Raw
    }

    /// <summary>
    /// HTML text element class.
    /// </summary>
    public class HtmlTextElement
    {
        /// <summary>
        /// Gets the element type.
        /// </summary>
        public HtmlTextElementType Type { get; }

        /// <summary>
        /// Gets the element value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates an HtmlTextElement instance.
        /// </summary>
        /// <param name="type">The element type.</param>
        /// <param name="value">The element value.</param>
        public HtmlTextElement(HtmlTextElementType type, string value)
        {
            Type = type;
            Value = value ?? string.Empty;
        }
    }
}