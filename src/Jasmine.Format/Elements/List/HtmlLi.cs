using Jasmine.Format.Elements.Text;
using Jasmine.Format.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Format.Elements.List
{
    /// <summary>
    /// Represents an HTML list item element.
    /// </summary>
    public sealed class HtmlLi : HtmlContainerElementBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlLi"/> class.
        /// </summary>
        public HtmlLi() : base(Array.Empty<object>(), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlLi"/> class with text content.
        /// </summary>
        /// <param name="text">The text content.</param>
        public HtmlLi(string text) : this(text, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlLi"/> class with text content and escape option.
        /// </summary>
        /// <param name="text">The text content.</param>
        /// <param name="escape">Whether to HTML encode the text. Default is true.</param>
        public HtmlLi(string text, bool escape) : base(
            !string.IsNullOrWhiteSpace(text)
                ? new[] { (object)new HtmlTextElement(HtmlTextElementType.Text, escape ? HtmlEncoder.HtmlEncode(text) : text) }
                : Array.Empty<object>(),
            null)
        {
        }

        internal HtmlLi(IReadOnlyList<object> elements, string style) : base(elements, style)
        {
        }

        /// <summary>
        /// Adds text content to the list item.
        /// </summary>
        /// <param name="text">The text to add.</param>
        /// <returns>A new HtmlLi instance with the added text.</returns>
        public HtmlLi Add(string text)
        {
            return Add(text, true);
        }

        /// <summary>
        /// Adds text content to the list item with escape option.
        /// </summary>
        /// <param name="text">The text to add.</param>
        /// <param name="escape">Whether to HTML encode the text. Default is true.</param>
        /// <returns>A new HtmlLi instance with the added text.</returns>
        public HtmlLi Add(string text, bool escape)
        {
            var newElement = new HtmlTextElement(HtmlTextElementType.Text, escape ? HtmlEncoder.HtmlEncode(text ?? string.Empty) : (text ?? string.Empty));
            return new HtmlLi(CreateNewElementList(newElement), Style);
        }

        /// <summary>
        /// Adds a span element to the list item.
        /// </summary>
        /// <param name="span">The span element to add.</param>
        /// <returns>A new HtmlLi instance with the added span.</returns>
        public HtmlLi Add(HtmlSpan span)
        {
            if (span == null)
                return this;

            return new HtmlLi(CreateNewElementList(span), Style);
        }

        /// <summary>
        /// Adds a paragraph element to the list item.
        /// </summary>
        /// <param name="paragraph">The paragraph element to add.</param>
        /// <returns>A new HtmlLi instance with the added paragraph.</returns>
        public HtmlLi Add(HtmlP paragraph)
        {
            if (paragraph == null)
                return this;

            return new HtmlLi(CreateNewElementList(paragraph), Style);
        }

        /// <summary>
        /// Adds raw HTML content to the list item.
        /// </summary>
        /// <param name="html">The raw HTML to add.</param>
        /// <returns>A new HtmlLi instance with the added HTML.</returns>
        public HtmlLi AddRaw(string html)
        {
            var newElement = new HtmlTextElement(HtmlTextElementType.Raw, html ?? string.Empty);
            return new HtmlLi(CreateNewElementList(newElement), Style);
        }

        /// <summary>
        /// Adds a range of span elements to the list item from an enumerable collection.
        /// Each item is converted to an HtmlSpan using the selector function.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to convert to spans.</param>
        /// <param name="selector">A function to create HtmlSpan from each item.</param>
        /// <returns>A new HtmlLi instance with the added spans.</returns>
        public HtmlLi AddSpanRange<T>(IEnumerable<T> items, Func<T, HtmlSpan> selector)
        {
            var newElements = CreateNewElementListWithPool(items, selector);
            return newElements == Elements ? this : new HtmlLi(newElements, Style);
        }

        /// <summary>
        /// Adds a range of span elements to the list item from an enumerable collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to convert to spans.</param>
        /// <param name="selector">A function to extract text from each item.</param>
        /// <param name="color">The text color for all spans.</param>
        /// <returns>A new HtmlLi instance with the added spans.</returns>
        public HtmlLi AddSpanRange<T>(IEnumerable<T> items, Func<T, string> selector, string color)
        {
            return AddSpanRange(items, selector, color, null);
        }

        /// <summary>
        /// Adds a range of span elements to the list item from an enumerable collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to convert to spans.</param>
        /// <param name="selector">A function to extract text from each item.</param>
        /// <param name="color">The text color for all spans.</param>
        /// <param name="style">Optional CSS style for all spans.</param>
        /// <returns>A new HtmlLi instance with the added spans.</returns>
        public HtmlLi AddSpanRange<T>(IEnumerable<T> items, Func<T, string> selector, string color, string style)
        {
            var newElements = CreateNewElementListWithPool(items, item => new HtmlSpan(selector(item), color, style));
            return newElements == Elements ? this : new HtmlLi(newElements, Style);
        }

        /// <summary>
        /// Adds a range of raw HTML content from an enumerable collection.
        /// The selector function can return HTML strings containing multiple elements.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to process.</param>
        /// <param name="selector">A function to generate HTML string from each item.</param>
        /// <returns>A new HtmlLi instance with the added raw HTML.</returns>
        public HtmlLi AddRawRange<T>(IEnumerable<T> items, Func<T, string> selector)
        {
            var newElements = CreateNewElementListWithRaw(items, selector);
            return newElements == Elements ? this : new HtmlLi(newElements, Style);
        }

        /// <summary>
        /// Creates a new empty HtmlLi with the same style.
        /// </summary>
        /// <returns>A new empty HtmlLi instance.</returns>
        public HtmlLi Clear()
        {
            return new HtmlLi(Array.Empty<object>(), Style);
        }

        /// <summary>
        /// Creates a new HtmlLi with the specified style.
        /// </summary>
        /// <param name="style">The CSS style to apply.</param>
        /// <returns>A new HtmlLi instance with the specified style.</returns>
        public HtmlLi WithStyle(string style)
        {
            return new HtmlLi(Elements, style);
        }

        /// <summary>
        /// Converts the list item element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the list item element.</returns>
        public override string ToHtml()
        {
            int estimatedLength = 9 + EstimateElementsHtmlLength();
            StringBuilder contentBuilder = StringBuilderCache.Acquire(estimatedLength);

            AppendElementsToBuilder(contentBuilder);

            string styleAttr = !string.IsNullOrWhiteSpace(Style) ? $" style=\"{Style}\"" : "";
            string result = $"<{HtmlTagNames.Li}{styleAttr}>{contentBuilder}</{HtmlTagNames.Li}>";
            StringBuilderCache.Release(contentBuilder);

            return result;
        }
    }
}