using Jasmine.Format.Elements.Basic;
using Jasmine.Format.Elements.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasmine.Format.Utilities
{
    /// <summary>
    /// Base class for HTML container elements that can hold multiple child elements.
    /// Provides common functionality for managing child elements.
    /// </summary>
    public abstract class HtmlContainerElementBase : HtmlStyledElement
    {
        private static readonly ObjectPool<List<object>> _listPool = new ObjectPool<List<object>>(() => new List<object>(), 32);

        /// <summary>
        /// Gets the child elements of this container.
        /// </summary>
        protected IReadOnlyList<object> Elements { get; }

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        public int Count => Elements.Count;

        /// <summary>
        /// Gets a value indicating whether the container is empty.
        /// </summary>
        public bool IsEmpty => Elements.Count == 0;

        /// <summary>
        /// Initializes a new instance with the specified elements and style.
        /// </summary>
        /// <param name="elements">The child elements.</param>
        /// <param name="style">The CSS style.</param>
        protected HtmlContainerElementBase(IReadOnlyList<object> elements, string style)
        {
            Elements = elements ?? Array.Empty<object>();
            Style = style;
        }

        /// <summary>
        /// Creates a new element list by adding an item to the existing elements.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A new read-only list with the added item.</returns>
        protected IReadOnlyList<object> CreateNewElementList(object item)
        {
            return Elements.Concat(new[] { item }).ToList().AsReadOnly();
        }

        /// <summary>
        /// Creates a new element list by adding multiple items to the existing elements using object pooling.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The items to add.</param>
        /// <param name="selector">A function to convert each item to an object.</param>
        /// <returns>A new read-only list with the added items.</returns>
        protected IReadOnlyList<object> CreateNewElementListWithPool<T>(IEnumerable<T> items, Func<T, object> selector)
        {
            if (items == null)
                return Elements;

            var pooledList = _listPool.Get();
            try
            {
                pooledList.AddRange(Elements);
                foreach (var item in items)
                {
                    if (item != null)
                    {
                        var element = selector(item);
                        if (element != null)
                        {
                            pooledList.Add(element);
                        }
                    }
                }
                return pooledList.ToList().AsReadOnly();
            }
            finally
            {
                _listPool.Return(pooledList);
            }
        }

        /// <summary>
        /// Creates a new element list by adding raw HTML content from a collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The items to process.</param>
        /// <param name="selector">A function to generate HTML string from each item.</param>
        /// <returns>A new read-only list with the added raw HTML elements.</returns>
        protected IReadOnlyList<object> CreateNewElementListWithRaw<T>(IEnumerable<T> items, Func<T, string> selector)
        {
            if (items == null)
                return Elements;

            var pooledList = _listPool.Get();
            try
            {
                pooledList.AddRange(Elements);
                foreach (var item in items)
                {
                    if (item != null)
                    {
                        string html = selector(item);
                        if (!string.IsNullOrEmpty(html))
                        {
                            pooledList.Add(new HtmlTextElement(HtmlTextElementType.Raw, html));
                        }
                    }
                }
                return pooledList.ToList().AsReadOnly();
            }
            finally
            {
                _listPool.Return(pooledList);
            }
        }

        /// <summary>
        /// Appends elements to a StringBuilder for HTML output.
        /// </summary>
        /// <param name="builder">The StringBuilder to append to.</param>
        protected void AppendElementsToBuilder(StringBuilder builder)
        {
            foreach (var element in Elements)
            {
                switch (element)
                {
                    case HtmlSpan span:
                        builder.Append(span.ToHtml());
                        break;
                    case HtmlA link:
                        builder.Append(link.ToHtml());
                        break;
                    case HtmlImg image:
                        builder.Append(image.ToHtml());
                        break;
                    case HtmlP paragraph:
                        builder.Append(paragraph.ToHtml());
                        break;
                    case HtmlTextElement textElement:
                        builder.Append(textElement.Value);
                        break;
                    default:
                        builder.Append(element?.ToString() ?? string.Empty);
                        break;
                }
            }
        }

        /// <summary>
        /// Estimates the HTML length for StringBuilder capacity planning.
        /// </summary>
        /// <returns>The estimated length.</returns>
        protected int EstimateElementsHtmlLength()
        {
            int estimated = 0;
            foreach (var element in Elements)
            {
                switch (element)
                {
                    case HtmlSpan span:
                        estimated += 32 + (span.Content?.Length ?? 0) + (span.Color?.Length ?? 0) + (span.Style?.Length ?? 0);
                        break;
                    case HtmlA link:
                        estimated += 64 + (link.Content?.Length ?? 0) + (link.Href?.Length ?? 0);
                        break;
                    case HtmlImg image:
                        estimated += 64 + (image.Src?.Length ?? 0) + (image.Alt?.Length ?? 0);
                        break;
                    case HtmlP paragraph:
                        estimated += 64;
                        break;
                    case HtmlTextElement textElement:
                        estimated += textElement.Value.Length;
                        break;
                    default:
                        estimated += (element?.ToString()?.Length ?? 0) + 16;
                        break;
                }
            }
            return estimated;
        }
    }
}