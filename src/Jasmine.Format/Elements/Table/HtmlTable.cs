using Jasmine.Format.Elements.Text;
using Jasmine.Format.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasmine.Format.Elements.Table
{
    /// <summary>
    /// Represents an HTML table row element.
    /// </summary>
    public sealed class HtmlTr : HtmlElementBase
    {
        private static readonly ObjectPool<List<object>> _listPool = new ObjectPool<List<object>>(() => new List<object>(), 16);

        private readonly IReadOnlyList<object> _cells;

        /// <summary>
        /// Gets the number of cells in the row.
        /// </summary>
        public int Count => _cells.Count;

        /// <summary>
        /// Gets a value indicating whether the row is empty.
        /// </summary>
        public bool IsEmpty => _cells.Count == 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTr"/> class.
        /// </summary>
        public HtmlTr()
        {
            _cells = Array.Empty<object>();
        }

        private HtmlTr(IReadOnlyList<object> cells)
        {
            _cells = cells ?? Array.Empty<object>();
        }

        /// <summary>
        /// Adds a text cell to the row.
        /// </summary>
        /// <param name="content">The text content of the cell.</param>
        /// <returns>A new HtmlTr instance with the added cell.</returns>
        public HtmlTr AddCell(string content)
        {
            return AddCell(new HtmlCell(content, CellType.Data));
        }

        /// <summary>
        /// Adds a text cell with color to the row.
        /// </summary>
        /// <param name="content">The text content of the cell.</param>
        /// <param name="color">The text color of the cell.</param>
        /// <returns>A new HtmlTr instance with the added cell.</returns>
        public HtmlTr AddCell(string content, string color)
        {
            return AddCell(new HtmlCell(content, CellType.Data, color: color));
        }

        /// <summary>
        /// Adds a text cell with color and style to the row.
        /// </summary>
        /// <param name="content">The text content of the cell.</param>
        /// <param name="color">The text color of the cell.</param>
        /// <param name="style">Optional CSS style for the cell.</param>
        /// <returns>A new HtmlTr instance with the added cell.</returns>
        public HtmlTr AddCell(string content, string color, string style)
        {
            return AddCell(new HtmlCell(content, CellType.Data, color: color, style: style));
        }

        /// <summary>
        /// Adds a cell element to the row.
        /// </summary>
        /// <param name="cell">The cell to add.</param>
        /// <returns>A new HtmlTr instance with the added cell.</returns>
        public HtmlTr AddCell(HtmlCell cell)
        {
            if (cell == null)
                return this;

            var newCells = _cells.Concat(new[] { (object)cell }).ToList().AsReadOnly();
            return new HtmlTr(newCells);
        }

        /// <summary>
        /// Adds a paragraph as a cell to the row.
        /// </summary>
        /// <param name="cell">The paragraph to add.</param>
        /// <returns>A new HtmlTr instance with the added cell.</returns>
        public HtmlTr AddCell(HtmlP cell)
        {
            if (cell == null)
                return this;

            var newCells = _cells.Concat(new[] { (object)cell }).ToList().AsReadOnly();
            return new HtmlTr(newCells);
        }

        /// <summary>
        /// Gets the cell at the specified index.
        /// </summary>
        /// <param name="index">The index of the cell.</param>
        /// <returns>The cell at the specified index.</returns>
        public object this[int index] => _cells[index];

        /// <summary>
        /// Converts the row element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the row element.</returns>
        public override string ToHtml()
        {
            int estimatedLength = EstimateHtmlLength();
            StringBuilder cellsBuilder = StringBuilderCache.Acquire(estimatedLength);

            foreach (var cell in _cells)
            {
                if (cell is HtmlCell td)
                {
                    cellsBuilder.Append(td.ToHtml());
                }
                else if (cell is HtmlP p)
                {
                    cellsBuilder.Append(p.ToHtml());
                }
                else
                {
                    cellsBuilder.Append(cell?.ToString() ?? string.Empty);
                }
            }

            string result = $"<{HtmlTagNames.Tr}>{cellsBuilder}</{HtmlTagNames.Tr}>";
            StringBuilderCache.Release(cellsBuilder);

            return result;
        }

        private int EstimateHtmlLength()
        {
            int estimated = 16;
            foreach (var cell in _cells)
            {
                if (cell is HtmlCell td)
                {
                    estimated += 16 + (td.Content?.Length ?? 0) + (td.Color?.Length ?? 0) + (td.Style?.Length ?? 0);
                }
                else if (cell is HtmlP p)
                {
                    estimated += 64;
                }
                else
                {
                    estimated += (cell?.ToString()?.Length ?? 0) + 16;
                }
            }
            return estimated;
        }
    }

    /// <summary>
    /// Represents an HTML table element.
    /// </summary>
    public sealed class HtmlTable : HtmlStyledElement
    {
        private static readonly ObjectPool<List<HtmlCell>> _headerPool = new ObjectPool<List<HtmlCell>>(() => new List<HtmlCell>(), 8);
        private static readonly ObjectPool<List<string>> _stringListPool = new ObjectPool<List<string>>(() => new List<string>(), 8);
        private static readonly ObjectPool<List<HtmlTr>> _rowPool = new ObjectPool<List<HtmlTr>>(() => new List<HtmlTr>(), 32);

        private readonly IReadOnlyList<HtmlCell> _headers;
        private readonly IReadOnlyList<HtmlTr> _rows;
        private readonly IReadOnlyList<string> _headerTexts;

        /// <summary>
        /// Gets the border style of the table.
        /// </summary>
        public string BorderStyle { get; }

        /// <summary>
        /// Gets the number of headers in the table.
        /// </summary>
        public int HeaderCount => _headers.Count;

        /// <summary>
        /// Gets the number of rows in the table.
        /// </summary>
        public int RowCount => _rows.Count;

        /// <summary>
        /// Gets the header texts.
        /// </summary>
        public IReadOnlyList<string> Headers => _headerTexts;

        /// <summary>
        /// Gets the rows of the table.
        /// </summary>
        public IReadOnlyList<HtmlTr> Rows => _rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTable"/> class.
        /// </summary>
        public HtmlTable()
        {
            _headers = Array.Empty<HtmlCell>();
            _rows = Array.Empty<HtmlTr>();
            _headerTexts = Array.Empty<string>();
        }

        private HtmlTable(IReadOnlyList<HtmlCell> headers, IReadOnlyList<HtmlTr> rows, IReadOnlyList<string> headerTexts, string style, string borderStyle)
        {
            _headers = headers ?? Array.Empty<HtmlCell>();
            _rows = rows ?? Array.Empty<HtmlTr>();
            _headerTexts = headerTexts ?? Array.Empty<string>();
            Style = style;
            BorderStyle = borderStyle;
        }

        /// <summary>
        /// Adds header cells to the table.
        /// </summary>
        /// <param name="headers">The header text values.</param>
        /// <returns>A new HtmlTable instance with the added headers.</returns>
        public HtmlTable AddHeader(params string[] headers)
        {
            if (headers == null || headers.Length == 0)
                return this;

            var pooledHeaders = _headerPool.Get();
            var pooledTexts = _stringListPool.Get();
            try
            {
                pooledHeaders.AddRange(_headers);
                pooledTexts.AddRange(_headerTexts);

                foreach (var header in headers)
                {
                    pooledHeaders.Add(new HtmlCell(header, CellType.Header));
                    pooledTexts.Add(header ?? string.Empty);
                }

                return new HtmlTable(pooledHeaders.ToList().AsReadOnly(), _rows, pooledTexts.ToList().AsReadOnly(), Style, BorderStyle);
            }
            finally
            {
                _headerPool.Return(pooledHeaders);
                _stringListPool.Return(pooledTexts);
            }
        }

        /// <summary>
        /// Adds a row with text cells to the table.
        /// </summary>
        /// <param name="cells">The text values for the cells.</param>
        /// <returns>A new HtmlTable instance with the added row.</returns>
        public HtmlTable AddRow(params string[] cells)
        {
            if (cells == null || cells.Length == 0)
                return this;

            var pooledRows = _rowPool.Get();
            try
            {
                pooledRows.AddRange(_rows);

                HtmlTr newRow = new HtmlTr();
                foreach (var cell in cells)
                {
                    newRow = newRow.AddCell(cell ?? string.Empty);
                }

                pooledRows.Add(newRow);
                return new HtmlTable(_headers, pooledRows.ToList().AsReadOnly(), _headerTexts, Style, BorderStyle);
            }
            finally
            {
                _rowPool.Return(pooledRows);
            }
        }

        /// <summary>
        /// Adds a row with paragraph cells to the table.
        /// </summary>
        /// <param name="cells">The paragraph cells.</param>
        /// <returns>A new HtmlTable instance with the added row.</returns>
        public HtmlTable AddRow(params HtmlP[] cells)
        {
            if (cells == null || cells.Length == 0)
                return this;

            var pooledRows = _rowPool.Get();
            try
            {
                pooledRows.AddRange(_rows);

                HtmlTr newRow = new HtmlTr();
                foreach (var cell in cells)
                {
                    newRow = newRow.AddCell(cell ?? new HtmlP());
                }

                pooledRows.Add(newRow);
                return new HtmlTable(_headers, pooledRows.ToList().AsReadOnly(), _headerTexts, Style, BorderStyle);
            }
            finally
            {
                _rowPool.Return(pooledRows);
            }
        }

        /// <summary>
        /// Creates a new HtmlTable with the specified style.
        /// </summary>
        /// <param name="style">The CSS style to apply.</param>
        /// <returns>A new HtmlTable instance with the specified style.</returns>
        public HtmlTable WithStyle(string style)
        {
            return new HtmlTable(_headers, _rows, _headerTexts, style, BorderStyle);
        }

        /// <summary>
        /// Creates a new HtmlTable with the specified border style.
        /// </summary>
        /// <param name="borderStyle">The border style to apply.</param>
        /// <returns>A new HtmlTable instance with the specified border style.</returns>
        public HtmlTable WithBorderStyle(string borderStyle)
        {
            return new HtmlTable(_headers, _rows, _headerTexts, Style, borderStyle);
        }

        /// <summary>
        /// Converts the table element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the table element.</returns>
        public override string ToHtml()
        {
            int estimatedLength = EstimateHtmlLength();
            StringBuilder builder = StringBuilderCache.Acquire(estimatedLength);

            StringBuilder styleBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Style))
            {
                styleBuilder.Append(Style);
            }
            if (!string.IsNullOrWhiteSpace(BorderStyle))
            {
                if (styleBuilder.Length > 0)
                    styleBuilder.Append(' ');
                styleBuilder.Append(BorderStyle);
            }

            string styleAttr = styleBuilder.Length > 0 ? $" style=\"{styleBuilder}\"" : "";
            builder.Append($"<{HtmlTagNames.Table}{styleAttr}>");

            if (_headers.Count > 0)
            {
                builder.Append($"<{HtmlTagNames.THead}><{HtmlTagNames.Tr}>");
                foreach (var header in _headers)
                {
                    builder.Append(header.ToHtml());
                }
                builder.Append($"</{HtmlTagNames.Tr}></{HtmlTagNames.THead}>");
            }

            if (_rows.Count > 0)
            {
                builder.Append($"<{HtmlTagNames.TBody}>");
                foreach (var row in _rows)
                {
                    builder.Append(row.ToHtml());
                }
                builder.Append($"</{HtmlTagNames.TBody}>");
            }

            builder.Append($"</{HtmlTagNames.Table}>");

            string result = builder.ToString();
            StringBuilderCache.Release(builder);

            return result;
        }

        private int EstimateHtmlLength()
        {
            int estimated = 128;

            foreach (var header in _headers)
            {
                estimated += 32 + (header.Content?.Length ?? 0) + (header.Color?.Length ?? 0) + (header.Style?.Length ?? 0);
            }

            foreach (var row in _rows)
            {
                estimated += 16 + (row.Count * 64);
            }

            if (!string.IsNullOrWhiteSpace(Style))
                estimated += Style.Length;
            if (!string.IsNullOrWhiteSpace(BorderStyle))
                estimated += BorderStyle.Length;

            return estimated;
        }
    }
}