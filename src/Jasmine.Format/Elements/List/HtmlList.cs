using Jasmine.Format.Elements.Text;
using Jasmine.Format.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasmine.Format.Elements.List
{
    /// <summary>
    /// Specifies the type of HTML list.
    /// </summary>
    public enum ListType
    {
        /// <summary>
        /// Unordered list (ul element).
        /// </summary>
        Unordered,
        /// <summary>
        /// Ordered list (ol element).
        /// </summary>
        Ordered
    }

    /// <summary>
    /// Represents an HTML list element (ul or ol).
    /// </summary>
    public sealed class HtmlList : HtmlStyledElement
    {
        private static readonly ObjectPool<List<HtmlLi>> _listPool = new ObjectPool<List<HtmlLi>>(() => new List<HtmlLi>(), 32);

        private readonly ListType _listType;
        private readonly IReadOnlyList<HtmlLi> _children;
        private readonly int _start;

        /// <summary>
        /// Gets the child list items.
        /// </summary>
        public IReadOnlyList<HtmlLi> Children => _children;

        /// <summary>
        /// Gets the number of list items.
        /// </summary>
        public int Count => _children.Count;

        /// <summary>
        /// Gets a value indicating whether the list is empty.
        /// </summary>
        public bool IsEmpty => _children.Count == 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlList"/> class.
        /// </summary>
        /// <param name="listType">The type of list. Default is Unordered.</param>
        public HtmlList(ListType listType = ListType.Unordered)
            : this(Array.Empty<HtmlLi>(), null, listType, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlList"/> class with style.
        /// </summary>
        /// <param name="style">Optional CSS style for the element.</param>
        /// <param name="listType">The type of list. Default is Unordered.</param>
        public HtmlList(string style, ListType listType = ListType.Unordered)
            : this(Array.Empty<HtmlLi>(), style, listType, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlList"/> class with start number.
        /// </summary>
        /// <param name="listType">The type of list.</param>
        /// <param name="start">The starting number for ordered lists.</param>
        public HtmlList(ListType listType, int start)
            : this(Array.Empty<HtmlLi>(), null, listType, start)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlList"/> class with style and start number.
        /// </summary>
        /// <param name="style">Optional CSS style for the element.</param>
        /// <param name="listType">The type of list.</param>
        /// <param name="start">The starting number for ordered lists.</param>
        public HtmlList(string style, ListType listType, int start)
            : this(Array.Empty<HtmlLi>(), style, listType, start)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlList"/> class with child items.
        /// </summary>
        /// <param name="children">The child list items.</param>
        /// <param name="listType">The type of list. Default is Unordered.</param>
        public HtmlList(IEnumerable<HtmlLi> children, ListType listType = ListType.Unordered)
            : this(CreateReadOnlyList(children), null, listType, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlList"/> class with child items and style.
        /// </summary>
        /// <param name="children">The child list items.</param>
        /// <param name="style">Optional CSS style for the element.</param>
        /// <param name="listType">The type of list. Default is Unordered.</param>
        public HtmlList(IEnumerable<HtmlLi> children, string style, ListType listType = ListType.Unordered)
            : this(CreateReadOnlyList(children), style, listType, 1)
        {
        }

        private static IReadOnlyList<HtmlLi> CreateReadOnlyList(IEnumerable<HtmlLi> source)
        {
            if (source == null)
                return Array.Empty<HtmlLi>();
            return source.ToList().AsReadOnly();
        }

        private HtmlList(IReadOnlyList<HtmlLi> children, string style, ListType listType, int start)
        {
            _children = children ?? Array.Empty<HtmlLi>();
            Style = style;
            _listType = listType;
            _start = start;
        }

        /// <summary>
        /// Adds a text item to the list.
        /// </summary>
        /// <param name="text">The text content of the item.</param>
        /// <returns>A new HtmlList instance with the added item.</returns>
        public HtmlList AddItem(string text)
        {
            return AddItem(new HtmlLi(text));
        }

        /// <summary>
        /// Adds a list item to the list.
        /// </summary>
        /// <param name="item">The list item to add.</param>
        /// <returns>A new HtmlList instance with the added item.</returns>
        public HtmlList AddItem(HtmlLi item)
        {
            var newChildren = _children.Concat(new[] { item ?? new HtmlLi(string.Empty) }).ToList().AsReadOnly();
            return new HtmlList(newChildren, Style, _listType, _start);
        }

        /// <summary>
        /// Adds a paragraph as a list item.
        /// </summary>
        /// <param name="paragraph">The paragraph to add as a list item.</param>
        /// <returns>A new HtmlList instance with the added item.</returns>
        public HtmlList AddItem(HtmlP paragraph)
        {
            var li = new HtmlLi().AddRaw(paragraph?.ToHtml() ?? string.Empty);
            return AddItem(li);
        }

        /// <summary>
        /// Adds multiple text items to the list.
        /// </summary>
        /// <param name="items">The text items to add.</param>
        /// <returns>A new HtmlList instance with the added items.</returns>
        public HtmlList AddRange(IEnumerable<string> items)
        {
            if (items == null)
                return this;

            var pooledList = _listPool.Get();
            try
            {
                pooledList.AddRange(_children);
                foreach (var item in items)
                {
                    pooledList.Add(new HtmlLi(item ?? string.Empty));
                }
                return new HtmlList(pooledList.ToArray(), Style, _listType, _start);
            }
            finally
            {
                _listPool.Return(pooledList);
            }
        }

        /// <summary>
        /// Adds multiple list items to the list.
        /// </summary>
        /// <param name="items">The list items to add.</param>
        /// <returns>A new HtmlList instance with the added items.</returns>
        public HtmlList AddRange(IEnumerable<HtmlLi> items)
        {
            if (items == null)
                return this;

            var pooledList = _listPool.Get();
            try
            {
                pooledList.AddRange(_children);
                foreach (var item in items)
                {
                    pooledList.Add(item ?? new HtmlLi(string.Empty));
                }
                return new HtmlList(pooledList.ToArray(), Style, _listType, _start);
            }
            finally
            {
                _listPool.Return(pooledList);
            }
        }

        /// <summary>
        /// Adds all items from another list to this list.
        /// </summary>
        /// <param name="other">The other list to merge.</param>
        /// <returns>A new HtmlList instance with merged items.</returns>
        public HtmlList AddRange(HtmlList other)
        {
            if (other == null || other._listType != _listType) return this;

            int newCount = _children.Count + other._children.Count;
            if (newCount == 0) return this;

            var pooledList = _listPool.Get();
            try
            {
                pooledList.Capacity = newCount;
                pooledList.AddRange(_children);
                pooledList.AddRange(other._children);
                return new HtmlList(pooledList.ToArray(), Style, _listType, _start);
            }
            finally
            {
                _listPool.Return(pooledList);
            }
        }

        /// <summary>
        /// Creates a new HtmlList with the specified style.
        /// </summary>
        /// <param name="style">The CSS style to apply.</param>
        /// <returns>A new HtmlList instance with the specified style.</returns>
        public HtmlList WithStyle(string style)
        {
            return new HtmlList(_children, style, _listType, _start);
        }

        /// <summary>
        /// Creates a new HtmlList with the specified start number.
        /// </summary>
        /// <param name="start">The starting number for ordered lists.</param>
        /// <returns>A new HtmlList instance with the specified start number.</returns>
        public HtmlList WithStart(int start)
        {
            return new HtmlList(_children, Style, _listType, start);
        }

        /// <summary>
        /// Creates a new empty HtmlList with the same style and type.
        /// </summary>
        /// <returns>A new empty HtmlList instance.</returns>
        public HtmlList Clear()
        {
            return new HtmlList(Array.Empty<HtmlLi>(), Style, _listType, _start);
        }

        /// <summary>
        /// Converts the list element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the list element.</returns>
        public override string ToHtml()
        {
            int estimatedLength = EstimateHtmlLength();
            StringBuilder contentBuilder = StringBuilderCache.Acquire(estimatedLength);

            foreach (var child in _children)
            {
                contentBuilder.Append(child?.ToHtml() ?? string.Empty);
            }

            string tag = _listType == ListType.Unordered ? HtmlTagNames.Ul : HtmlTagNames.Ol;
            string styleAttr = !string.IsNullOrWhiteSpace(Style) ? $" style=\"{HtmlEncoder.HtmlEncode(Style)}\"" : "";
            string startAttr = _listType == ListType.Ordered && _start > 1 ? $" start=\"{_start}\"" : "";
            string result = $"<{tag}{startAttr}{styleAttr}>{contentBuilder}</{tag}>";
            StringBuilderCache.Release(contentBuilder);

            return result;
        }

        private int EstimateHtmlLength()
        {
            int estimated = 16 + (Style?.Length ?? 0);
            foreach (var child in _children)
            {
                estimated += (child?.ToString()?.Length ?? 0) + 16;
            }
            return estimated;
        }
    }
}