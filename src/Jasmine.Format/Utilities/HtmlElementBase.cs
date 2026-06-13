namespace Jasmine.Format.Utilities
{
    /// <summary>
    /// Base class for all HTML elements.
    /// </summary>
    public abstract class HtmlElementBase
    {
        /// <summary>
        /// Converts the element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the element.</returns>
        public abstract string ToHtml();

        /// <summary>
        /// Returns the HTML string representation of the element.
        /// </summary>
        /// <returns>HTML string representation of the element.</returns>
        public override string ToString()
        {
            return ToHtml();
        }
    }
}