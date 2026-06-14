using Jasmine.Format.Elements.Basic;
using Jasmine.Format.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Format.Elements.Text
{
    /// <summary>
    /// Represents an HTML paragraph element.
    /// </summary>
    public sealed class HtmlP : HtmlContainerElementBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlP"/> class.
        /// </summary>
        public HtmlP() : base(Array.Empty<object>(), null)
        {
        }

        internal HtmlP(IReadOnlyList<object> elements, string style) : base(elements, style)
        {
        }

        /// <summary>
        /// Adds text content to the paragraph.
        /// </summary>
        /// <param name="text">The text to add.</param>
        /// <returns>A new HtmlP instance with the added text.</returns>
        public HtmlP Add(string text)
        {
            return Add(text, true);
        }

        /// <summary>
        /// Adds text content to the paragraph with escape option.
        /// </summary>
        /// <param name="text">The text to add.</param>
        /// <param name="escape">Whether to HTML encode the text. Default is true.</param>
        /// <returns>A new HtmlP instance with the added text.</returns>
        public HtmlP Add(string text, bool escape)
        {
            var newElement = new HtmlTextElement(HtmlTextElementType.Text, escape ? HtmlEncoder.HtmlEncode(text ?? string.Empty) : (text ?? string.Empty));
            return new HtmlP(CreateNewElementList(newElement), Style);
        }

        /// <summary>
        /// Adds a span element to the paragraph.
        /// </summary>
        /// <param name="span">The span element to add.</param>
        /// <returns>A new HtmlP instance with the added span.</returns>
        public HtmlP Add(HtmlSpan span)
        {
            if (span == null)
                return this;

            return new HtmlP(CreateNewElementList(span), Style);
        }

        /// <summary>
        /// Adds a link element to the paragraph.
        /// </summary>
        /// <param name="link">The link element to add.</param>
        /// <returns>A new HtmlP instance with the added link.</returns>
        public HtmlP Add(HtmlA link)
        {
            if (link == null)
                return this;

            return new HtmlP(CreateNewElementList(link), Style);
        }

        /// <summary>
        /// Adds an image element to the paragraph.
        /// </summary>
        /// <param name="image">The image element to add.</param>
        /// <returns>A new HtmlP instance with the added image.</returns>
        public HtmlP Add(HtmlImg image)
        {
            if (image == null)
                return this;

            return new HtmlP(CreateNewElementList(image), Style);
        }

        /// <summary>
        /// Adds raw HTML content to the paragraph.
        /// </summary>
        /// <param name="html">The raw HTML to add.</param>
        /// <returns>A new HtmlP instance with the added HTML.</returns>
        public HtmlP AddRaw(string html)
        {
            var newElement = new HtmlTextElement(HtmlTextElementType.Raw, html ?? string.Empty);
            return new HtmlP(CreateNewElementList(newElement), Style);
        }

        /// <summary>
        /// Adds a range of span elements to the paragraph from an enumerable collection.
        /// Each item is converted to an HtmlSpan using the selector function.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to convert to spans.</param>
        /// <param name="selector">A function to create HtmlSpan from each item.</param>
        /// <returns>A new HtmlP instance with the added spans.</returns>
        public HtmlP AddSpanRange<T>(IEnumerable<T> items, Func<T, HtmlSpan> selector)
        {
            var newElements = CreateNewElementListWithPool(items, selector);
            return newElements == Elements ? this : new HtmlP(newElements, Style);
        }

        /// <summary>
        /// Adds a range of span elements to the paragraph from an enumerable collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to convert to spans.</param>
        /// <param name="selector">A function to extract text from each item.</param>
        /// <returns>A new HtmlP instance with the added spans.</returns>
        public HtmlP AddSpanRange<T>(IEnumerable<T> items, Func<T, string> selector)
        {
            return AddSpanRange(items, selector, null, null);
        }

        /// <summary>
        /// Adds a range of span elements to the paragraph from an enumerable collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to convert to spans.</param>
        /// <param name="selector">A function to extract text from each item.</param>
        /// <param name="color">The text color for all spans.</param>
        /// <returns>A new HtmlP instance with the added spans.</returns>
        public HtmlP AddSpanRange<T>(IEnumerable<T> items, Func<T, string> selector, string color)
        {
            return AddSpanRange(items, selector, color, null);
        }

        /// <summary>
        /// Adds a range of span elements to the paragraph from an enumerable collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to convert to spans.</param>
        /// <param name="selector">A function to extract text from each item.</param>
        /// <param name="color">The text color for all spans.</param>
        /// <param name="style">Optional CSS style for all spans.</param>
        /// <returns>A new HtmlP instance with the added spans.</returns>
        public HtmlP AddSpanRange<T>(IEnumerable<T> items, Func<T, string> selector, string color, string style)
        {
            var newElements = CreateNewElementListWithPool(items, item => new HtmlSpan(selector(item), color, style));
            return newElements == Elements ? this : new HtmlP(newElements, Style);
        }

        /// <summary>
        /// Adds a range of raw HTML content from an enumerable collection.
        /// The selector function can return HTML strings containing multiple elements.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The collection of items to process.</param>
        /// <param name="selector">A function to generate HTML string from each item.</param>
        /// <returns>A new HtmlP instance with the added raw HTML.</returns>
        public HtmlP AddRawRange<T>(IEnumerable<T> items, Func<T, string> selector)
        {
            var newElements = CreateNewElementListWithRaw(items, selector);
            return newElements == Elements ? this : new HtmlP(newElements, Style);
        }

        /// <summary>
        /// Creates a new empty HtmlP with the same style.
        /// </summary>
        /// <returns>A new empty HtmlP instance.</returns>
        public HtmlP Clear()
        {
            return new HtmlP(Array.Empty<object>(), Style);
        }

        /// <summary>
        /// Creates a new HtmlP with the specified style.
        /// </summary>
        /// <param name="style">The CSS style to apply.</param>
        /// <returns>A new HtmlP instance with the specified style.</returns>
        public HtmlP WithStyle(string style)
        {
            return new HtmlP(Elements, style);
        }

        /// <summary>
        /// Creates a new HtmlP with the same content but without any style.
        /// </summary>
        /// <returns>A new HtmlP instance with the same content but no style.</returns>
        public HtmlP ClearStyle()
        {
            return new HtmlP(Elements, null);
        }

        /// <summary>
        /// Extracts plain text content from the paragraph, stripping all HTML tags.
        /// </summary>
        /// <returns>The plain text content without any HTML tags.</returns>
        public string ToPlainText()
        {
            if (IsEmpty)
                return string.Empty;

            StringBuilder sb = StringBuilderCache.Acquire();
            try
            {
                foreach (var element in Elements)
                {
                    if (element is HtmlTextElement textElement)
                    {
                        sb.Append(textElement.Value);
                    }
                    else if (element is HtmlSpan span)
                    {
                        sb.Append(span.Content);
                    }
                    else if (element is HtmlA link)
                    {
                        sb.Append(link.Content);
                    }
                    else if (element is HtmlImg img)
                    {
                        sb.Append(img.Alt ?? string.Empty);
                    }
                }
                return sb.ToString();
            }
            finally
            {
                StringBuilderCache.Release(sb);
            }
        }

        /// <summary>
        /// Converts the paragraph element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the paragraph element.</returns>
        public override string ToHtml()
        {
            int estimatedLength = 8 + EstimateElementsHtmlLength();
            StringBuilder contentBuilder = StringBuilderCache.Acquire(estimatedLength);

            AppendElementsToBuilder(contentBuilder);

            string styleAttr = !string.IsNullOrWhiteSpace(Style) ? $" style=\"{Style}\"" : "";
            string result = $"<{HtmlTagNames.P}{styleAttr}>{contentBuilder}</{HtmlTagNames.P}>";
            StringBuilderCache.Release(contentBuilder);

            return result;
        }
    }
}