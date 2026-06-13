using Jasmine.Format.Elements.Text;
using Jasmine.Format.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasmine.Format.Elements.Container
{
    /// <summary>
    /// Represents an HTML div element.
    /// </summary>
    public sealed class HtmlDiv : HtmlStyledElement
    {
        private static readonly ObjectPool<List<HtmlP>> _listPool = new ObjectPool<List<HtmlP>>(() => new List<HtmlP>(), 16);

        private readonly IReadOnlyList<HtmlP> _children;

        /// <summary>
        /// Gets the child paragraph elements.
        /// </summary>
        public IReadOnlyList<HtmlP> Children => _children;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlDiv"/> class.
        /// </summary>
        public HtmlDiv()
            : this(Array.Empty<HtmlP>(), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlDiv"/> class with style.
        /// </summary>
        /// <param name="style">Optional CSS style for the element.</param>
        public HtmlDiv(string style)
            : this(Array.Empty<HtmlP>(), style)
        {
        }

        private HtmlDiv(IReadOnlyList<HtmlP> children, string style)
        {
            _children = children ?? Array.Empty<HtmlP>();
            Style = style;
        }

        /// <summary>
        /// Adds a paragraph element to the div.
        /// </summary>
        /// <param name="paragraph">The paragraph to add.</param>
        /// <returns>A new HtmlDiv instance with the added paragraph.</returns>
        /// <exception cref="ArgumentNullException">Thrown when paragraph is null.</exception>
        public HtmlDiv Add(HtmlP paragraph)
        {
            if (paragraph == null)
                throw new ArgumentNullException(nameof(paragraph));

            var newChildren = _children.Concat(new[] { paragraph }).ToList().AsReadOnly();
            return new HtmlDiv(newChildren, Style);
        }

        /// <summary>
        /// Adds text content as a paragraph to the div.
        /// </summary>
        /// <param name="text">The text to add.</param>
        /// <returns>A new HtmlDiv instance with the added text.</returns>
        public HtmlDiv Add(string text)
        {
            var paragraph = new HtmlP().Add(text);
            return Add(paragraph);
        }

        /// <summary>
        /// Creates a new HtmlDiv with the specified style.
        /// </summary>
        /// <param name="style">The CSS style to apply.</param>
        /// <returns>A new HtmlDiv instance with the specified style.</returns>
        public HtmlDiv WithStyle(string style)
        {
            return new HtmlDiv(_children, style);
        }

        /// <summary>
        /// Converts the div element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the div element.</returns>
        public override string ToHtml()
        {
            if (_children.Count == 0)
            {
                var styleAttr = !string.IsNullOrWhiteSpace(Style) ? $" style=\"{HtmlEncoder.HtmlEncode(Style)}\"" : "";
            return $"<{HtmlTagNames.Div}{styleAttr}></{HtmlTagNames.Div}>";
            }

            int estimatedLength = EstimateHtmlLength();
            StringBuilder contentBuilder = StringBuilderCache.Acquire(estimatedLength);

            foreach (var child in _children)
            {
                contentBuilder.Append(child.ToHtml());
            }

            var divStyleAttr = !string.IsNullOrWhiteSpace(Style) ? $" style=\"{HtmlEncoder.HtmlEncode(Style)}\"" : "";
            string result = $"<{HtmlTagNames.Div}{divStyleAttr}>{contentBuilder}</{HtmlTagNames.Div}>";
            StringBuilderCache.Release(contentBuilder);

            return result;
        }

        private int EstimateHtmlLength()
        {
            int estimated = 16 + (Style?.Length ?? 0);
            foreach (var child in _children)
            {
                estimated += child.ToHtml().Length;
            }
            return estimated;
        }
    }
}