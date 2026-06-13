# Jasmine.Format

一个用于生成 HTML 内容的 .NET 库，提供流畅、线程安全的 API。专为高性能场景设计，采用对象池和不可变模式。

A .NET library for generating HTML content with a fluent, thread-safe API. Designed for high-performance scenarios with object pooling and immutable patterns.

## 功能特性

- **自动 HTML 编码**：默认防止 XSS 攻击
- **流畅 API**：链式方法调用，代码更简洁
- **线程安全**：不可变模式确保并发访问安全
- **条件渲染**：根据条件动态添加内容
- **批量操作**：高效处理集合数据
- **性能优化**：对象池和 StringBuilder 缓存
- **多框架支持**：.NET Standard 2.0、.NET 6/7/8/9/10

## Features

- **Automatic HTML Encoding**: Protects against XSS attacks by default
- **Fluent API**: Chainable method calls for clean code
- **Thread-Safe**: Immutable pattern ensures safe concurrent access
- **Conditional Rendering**: Dynamically add content based on conditions
- **Batch Operations**: Efficiently process collections
- **Performance Optimized**: Object pooling and StringBuilder caching
- **Multi-framework Support**: .NET Standard 2.0, .NET 6/7/8/9/10

## 安装

通过 NuGet 安装：

```bash
Install-Package Jasmine.Format
```

或使用 .NET CLI：

```bash
dotnet add package Jasmine.Format
```

## Installation

Install via NuGet:

```bash
Install-Package Jasmine.Format
```

Or using .NET CLI:

```bash
dotnet add package Jasmine.Format
```

## 快速开始

### 基础用法

```csharp
using Jasmine.Format;

// 创建简单段落
var paragraph = new HtmlP()
    .Add("Hello, World!");

string html = paragraph.ToHtml();
// 输出: <p>Hello, World!</p>
```

### 进阶示例

```csharp
var isAdmin = true;
var html = new HtmlP().WithStyle("line-height: 1.5;")
    .Add("Hello, ")
    .Add(new HtmlSpan("World", "#0066cc", "font-weight: bold;"))
    .Add("! ")
    .Add(isAdmin ? "(Admin)" : "")
    .AddRaw("<br/>")
    .Add("Current time: ")
    .Add(new HtmlSpan(DateTime.Now.ToString(), "#666"))
    .ToHtml();
```

## Quick Start

### Basic Usage

```csharp
using Jasmine.Format;

// Create a simple paragraph
var paragraph = new HtmlP()
    .Add("Hello, World!");

string html = paragraph.ToHtml();
// Output: <p>Hello, World!</p>
```

### Advanced Example

```csharp
var isAdmin = true;
var html = new HtmlP().WithStyle("line-height: 1.5;")
    .Add("Hello, ")
    .Add(new HtmlSpan("World", "#0066cc", "font-weight: bold;"))
    .Add("! ")
    .Add(isAdmin ? "(Admin)" : "")
    .AddRaw("<br/>")
    .Add("Current time: ")
    .Add(new HtmlSpan(DateTime.Now.ToString(), "#666"))
    .ToHtml();
```

## API 参考

### HtmlP 类

构建 HTML `<p>` 元素的核心类。

#### 构造函数

```csharp
// 空段落
var p = new HtmlP();

// 带样式的段落
var p = new HtmlP().WithStyle("font-size: 14px; color: #333;");
```

#### 属性

- `Style` (string): 获取段落的 CSS 样式
- `Count` (int): 获取段落中的元素数量

#### 添加文本

```csharp
// 添加文本（自动进行 HTML 编码）
var p = new HtmlP().Add("Hello <World>");
// 输出: <p>Hello &lt;World&gt;</p>

// 添加不编码的文本
var p = new HtmlP().Add("<b>bold</b>", escape: false);
// 输出: <p><b>bold</b></p>
```

## API Reference

### HtmlP Class

The core class for building HTML `<p>` elements.

#### Constructors

```csharp
// Empty paragraph
var p = new HtmlP();

// Paragraph with style
var p = new HtmlP().WithStyle("font-size: 14px; color: #333;");
```

#### Properties

- `Style` (string): Gets the paragraph's CSS style
- `Count` (int): Gets the number of elements in the paragraph

#### Adding Text

```csharp
// Add text (automatically HTML encoded)
var p = new HtmlP().Add("Hello <World>");
// Output: <p>Hello &lt;World&gt;</p>

// Add text without encoding
var p = new HtmlP().Add("<b>bold</b>", escape: false);
// Output: <p><b>bold</b></p>
```

#### 添加 Span 元素

```csharp
// 简单的 Span
var p = new HtmlP().Add(new HtmlSpan("highlighted"));
// 输出: <p><span>highlighted</span></p>

// 带颜色的 Span
var p = new HtmlP().Add(new HtmlSpan("red text", "#ff0000"));
// 输出: <p><span style="color:#ff0000;">red text</span></p>

// 带颜色和样式的 Span
var p = new HtmlP().Add(new HtmlSpan("styled", "#00ff00", "font-weight: bold;"));
// 输出: <p><span style="color:#00ff00;font-weight: bold;">styled</span></p>

// 重复添加 Span
var p = new HtmlP().Add(new HtmlSpan("item")).Add(new HtmlSpan("item")).Add(new HtmlSpan("item"));
// 输出: <p><span>item</span><span>item</span><span>item</span></p>

// 添加已创建的 HtmlSpan 对象
var span = new HtmlSpan("custom", "#0000ff");
var p = new HtmlP().Add(span);
```

#### Adding Span Elements

```csharp
// Simple span
var p = new HtmlP().Add(new HtmlSpan("highlighted"));
// Output: <p><span>highlighted</span></p>

// Span with color
var p = new HtmlP().Add(new HtmlSpan("red text", "#ff0000"));
// Output: <p><span style="color:#ff0000;">red text</span></p>

// Span with color and style
var p = new HtmlP().Add(new HtmlSpan("styled", "#00ff00", "font-weight: bold;"));
// Output: <p><span style="color:#00ff00;font-weight: bold;">styled</span></p>

// Repeat span
var p = new HtmlP().Add(new HtmlSpan("item")).Add(new HtmlSpan("item")).Add(new HtmlSpan("item"));
// Output: <p><span>item</span><span>item</span><span>item</span></p>

// Add existing HtmlSpan object
var span = new HtmlSpan("custom", "#0000ff");
var p = new HtmlP().Add(span);
```

#### 条件渲染

```csharp
// 使用三元运算符进行条件渲染
var isError = true;
var p = isError
    ? new HtmlP().Add("错误信息").WithStyle("color:red;")
    : new HtmlP().Add("正常");

// 条件添加元素
var showDetails = true;
var p = new HtmlP().Add("标题");
if (showDetails)
{
    p = p.Add(new HtmlSpan("详细信息", "#666"));
}
```

#### Conditional Rendering

```csharp
// Using ternary operator for conditional rendering
var isError = true;
var p = isError
    ? new HtmlP().Add("Error message").WithStyle("color:red;")
    : new HtmlP().Add("Normal");

// Conditional element addition
var showDetails = true;
var p = new HtmlP().Add("Title");
if (showDetails)
{
    p = p.Add(new HtmlSpan("Details", "#666"));
}
```

#### 批量操作

```csharp
// 批量添加带样式的 Span
var items = new[] { "Apple", "Banana", "Orange" };
var p = new HtmlP().AddSpanRange(items, x => x, "#ff6600");
// 输出: <p><span style="color:#ff6600;">Apple</span>，<span style="color:#ff6600;">Banana</span>，<span style="color:#ff6600;">Orange</span></p>

// 使用自定义选择器
var users = new[] { new { Name = "Alice" }, new { Name = "Bob" } };
var p = new HtmlP().AddSpanRange(users, u => u.Name, "#0000ff");
// 输出: <p><span style="color:#0000ff;">Alice</span>，<span style="color:#0000ff;">Bob</span></p>
```

#### Batch Operations

```csharp
// Batch add spans with color
var items = new[] { "Apple", "Banana", "Orange" };
var p = new HtmlP().AddSpanRange(items, x => x, "#ff6600");
// Output: <p><span style="color:#ff6600;">Apple</span>，<span style="color:#ff6600;">Banana</span>，<span style="color:#ff6600;">Orange</span></p>

// Using custom selector
var users = new[] { new { Name = "Alice" }, new { Name = "Bob" } };
var p = new HtmlP().AddSpanRange(users, u => u.Name, "#0000ff");
// Output: <p><span style="color:#0000ff;">Alice</span>，<span style="color:#0000ff;">Bob</span></p>
```

#### 添加原始 HTML

```csharp
var p = new HtmlP()
    .Add("Before")
    .AddRaw("<br/>")
    .Add("After");
// 输出: <p>Before<br/>After</p>
```

#### Raw HTML

```csharp
var p = new HtmlP()
    .Add("Before")
    .AddRaw("<br/>")
    .Add("After");
// Output: <p>Before<br/>After</p>
```

#### 样式操作

```csharp
// 创建带样式的段落
var p = new HtmlP().WithStyle("margin: 10px;")
    .Add("Content");
// 输出: <p style="margin: 10px;">Content</p>

// 创建新实例并设置样式
var p = new HtmlP().Add("content").WithStyle("text-align: center;");
// 输出: <p style="text-align: center;">content</p>
```

#### Style Manipulation

```csharp
// Create with style
var p = new HtmlP().WithStyle("margin: 10px;")
    .Add("Content");
// Output: <p style="margin: 10px;">Content</p>

// Create new instance with style
var p = new HtmlP().Add("content").WithStyle("text-align: center;");
// Output: <p style="text-align: center;">content</p>
```

#### 生成 HTML

```csharp
var p = new HtmlP().Add("Hello");

// 方式1：调用 ToHtml()
string html = p.ToHtml();

// 方式2：调用 ToString()
string html = p.ToString();
```

#### Generate HTML

```csharp
var p = new HtmlP().Add("Hello");

// Option 1: ToHtml()
string html = p.ToHtml();

// Option 2: ToString()
string html = p.ToString();
```

### HtmlSpan 类

创建独立的 `<span>` 元素。

```csharp
// 构造函数
var span = new HtmlSpan("content");
var span = new HtmlSpan("content", "#ff0000");
var span = new HtmlSpan("content", "#ff0000", "font-weight: bold;");

// 生成 HTML
string html = span.ToHtml();
// 输出: <span style="color:#ff0000;font-weight: bold;">content</span>
```

### HtmlSpan Class

Creates standalone `<span>` elements.

```csharp
// Constructors
var span = new HtmlSpan("content");
var span = new HtmlSpan("content", "#ff0000");
var span = new HtmlSpan("content", "#ff0000", "font-weight: bold;");

// Generate HTML
string html = span.ToHtml();
// Output: <span style="color:#ff0000;font-weight: bold;">content</span>
```

### HtmlList 类

创建 HTML 列表（ul 或 ol 元素）。

#### 构造函数

```csharp
// 无序列表
var ul = new HtmlList(ListType.Unordered);

// 有序列表
var ol = new HtmlList(ListType.Ordered);

// 带样式的列表
var ul = new HtmlList("list-style-type:square;", ListType.Unordered);

// 有序列表，从 5 开始编号
var ol = new HtmlList(ListType.Ordered, 5);
```

#### 添加列表项

```csharp
// 添加单个列表项
var ul = new HtmlList(ListType.Unordered)
    .AddItem("项目一")
    .AddItem("项目二");

// 添加多个列表项
var items = new[] { "Apple", "Banana", "Orange" };
var ul = new HtmlList(ListType.Unordered).AddRange(items);

// 添加段落作为列表项
var ul = new HtmlList(ListType.Unordered)
    .AddItem(new HtmlP().Add("段落内容"));
```

#### 列表样式

```csharp
// 方形列表标记
var ul = new HtmlList("list-style-type:square;", ListType.Unordered)
    .AddItem("项目一")
    .AddItem("项目二");

// 输出: <ul style="list-style-type:square;"><li>项目一</li><li>项目二</li></ul>
```

### HtmlList Class

Creates HTML lists (ul or ol elements).

#### Constructors

```csharp
// Unordered list
var ul = new HtmlList(ListType.Unordered);

// Ordered list
var ol = new HtmlList(ListType.Ordered);

// List with style
var ul = new HtmlList("list-style-type:square;", ListType.Unordered);

// Ordered list starting from 5
var ol = new HtmlList(ListType.Ordered, 5);
```

#### Adding Items

```csharp
// Add single items
var ul = new HtmlList(ListType.Unordered)
    .AddItem("Item 1")
    .AddItem("Item 2");

// Add multiple items
var items = new[] { "Apple", "Banana", "Orange" };
var ul = new HtmlList(ListType.Unordered).AddRange(items);

// Add paragraph as item
var ul = new HtmlList(ListType.Unordered)
    .AddItem(new HtmlP().Add("Paragraph content"));
```

#### List Styles

```csharp
// Square list markers
var ul = new HtmlList("list-style-type:square;", ListType.Unordered)
    .AddItem("Item 1")
    .AddItem("Item 2");

// Output: <ul style="list-style-type:square;"><li>Item 1</li><li>Item 2</li></ul>
```

### HtmlDiv 类

创建 HTML div 元素。

#### 构造函数

```csharp
// 空 div
var div = new HtmlDiv();

// 带样式的 div
var div = new HtmlDiv("border:1px solid #ccc; padding:10px;");
```

#### 添加内容

```csharp
// 添加文本（自动包装为段落）
var div = new HtmlDiv().Add("内容");
// 输出: <div><p>内容</p></div>

// 添加段落
var div = new HtmlDiv()
    .Add(new HtmlP().Add("第一段"))
    .Add(new HtmlP().Add("第二段"));
// 输出: <div><p>第一段</p><p>第二段</p></div>
```

### HtmlDiv Class

Creates HTML div elements.

#### Constructors

```csharp
// Empty div
var div = new HtmlDiv();

// Div with style
var div = new HtmlDiv("border:1px solid #ccc; padding:10px;");
```

#### Adding Content

```csharp
// Add text (automatically wrapped in paragraph)
var div = new HtmlDiv().Add("Content");
// Output: <div><p>Content</p></div>

// Add paragraphs
var div = new HtmlDiv()
    .Add(new HtmlP().Add("First paragraph"))
    .Add(new HtmlP().Add("Second paragraph"));
// Output: <div><p>First paragraph</p><p>Second paragraph</p></div>
```

### HtmlLi 类

创建 HTML 列表项元素。

#### 构造函数

```csharp
// 空列表项
var li = new HtmlLi();

// 带文本的列表项
var li = new HtmlLi("项目内容");

// 带 HTML 的列表项（不编码）
var li = new HtmlLi("<b>粗体</b>", false);
```

### HtmlLi Class

Creates HTML list item elements.

#### Constructors

```csharp
// Empty list item
var li = new HtmlLi();

// List item with text
var li = new HtmlLi("Item content");

// List item with HTML (not encoded)
var li = new HtmlLi("<b>bold</b>", false);
```

### HtmlA 类

创建 HTML 链接元素。

#### 构造函数

```csharp
// 基础链接
var link = new HtmlA("链接文本", "https://example.com");
// 输出: <a href="https://example.com">链接文本</a>

// 带 target 属性
var link = new HtmlA("新窗口链接", "https://example.com", "_blank");
// 输出: <a href="https://example.com" target="_blank">新窗口链接</a>

// 带颜色
var link = new HtmlA("红色链接", "https://example.com", "_blank", "#ff0000");
// 输出: <a href="https://example.com" target="_blank" style="color:#ff0000;">红色链接</a>

// 带样式
var link = new HtmlA("样式链接", "https://example.com", "_blank", "#ff0000", "text-decoration:none;");
```

### HtmlA Class

Creates HTML anchor elements.

#### Constructors

```csharp
// Basic link
var link = new HtmlA("Link Text", "https://example.com");
// Output: <a href="https://example.com">Link Text</a>

// With target attribute
var link = new HtmlA("New Window Link", "https://example.com", "_blank");
// Output: <a href="https://example.com" target="_blank">New Window Link</a>

// With color
var link = new HtmlA("Red Link", "https://example.com", "_blank", "#ff0000");
// Output: <a href="https://example.com" target="_blank" style="color:#ff0000;">Red Link</a>

// With style
var link = new HtmlA("Styled Link", "https://example.com", "_blank", "#ff0000", "text-decoration:none;");
```

### HtmlFormatHelper 类

提供静态方法用于格式化 HTML 模板。

#### Format 方法（位置参数）

```csharp
string template = "<p>Hello {0}, age: {1}</p>";
string html = HtmlFormatHelper.Format(template, "Alice", 25);
// 输出: <p>Hello Alice, age: 25</p>

// 自动编码
string html = HtmlFormatHelper.Format("<p>{0}</p>", "<script>alert('XSS')</script>");
// 输出: <p>&lt;script&gt;alert(&#39;XSS&#39;)&lt;/script&gt;</p>
```

### HtmlFormatHelper Class

Static methods for HTML template formatting.

#### Format (Positional Parameters)

```csharp
string template = "<p>Hello {0}, age: {1}</p>";
string html = HtmlFormatHelper.Format(template, "Alice", 25);
// Output: <p>Hello Alice, age: 25</p>

// Auto-encoded
string html = HtmlFormatHelper.Format("<p>{0}</p>", "<script>alert('XSS')</script>");
// Output: <p>&lt;script&gt;alert(&#39;XSS&#39;)&lt;/script&gt;</p>
```

#### FormatTemplate 方法（命名参数）

```csharp
// 使用 KeyValuePair 数组
var kv = new KeyValuePair<string, string>[]
{
    new KeyValuePair<string, string>("name", "Bob"),
    new KeyValuePair<string, string>("age", "30")
};
string html = HtmlFormatHelper.FormatTemplate("<p>Hello {name}, age: {age}</p>", kv);

// 使用 Dictionary
var dict = new Dictionary<string, string>
{
    { "name", "Charlie" },
    { "age", "35" }
};
string html = HtmlFormatHelper.FormatTemplate("<p>Hello {name}, age: {age}</p>", dict);

// 禁用编码
string html = HtmlFormatHelper.FormatTemplate("<p>{content}</p>", false, 
    new KeyValuePair<string, string>("content", "<b>bold</b>"));
// 输出: <p><b>bold</b></p>
```

#### FormatTemplate (Named Parameters)

```csharp
// Using KeyValuePair array
var kv = new KeyValuePair<string, string>[]
{
    new KeyValuePair<string, string>("name", "Bob"),
    new KeyValuePair<string, string>("age", "30")
};
string html = HtmlFormatHelper.FormatTemplate("<p>Hello {name}, age: {age}</p>", kv);

// Using Dictionary
var dict = new Dictionary<string, string>
{
    { "name", "Charlie" },
    { "age", "35" }
};
string html = HtmlFormatHelper.FormatTemplate("<p>Hello {name}, age: {age}</p>", dict);

// Disable encoding
string html = HtmlFormatHelper.FormatTemplate("<p>{content}</p>", false, 
    new KeyValuePair<string, string>("content", "<b>bold</b>"));
// Output: <p><b>bold</b></p>
```

## 线程安全

库采用不可变模式：

```csharp
// 原始实例保持不变
var original = new HtmlP();
var modified = original.Add("Hello");

// 两个实例相互独立
Assert.Equal("<p></p>", original.ToHtml());
Assert.Equal("<p>Hello</p>", modified.ToHtml());
Assert.NotSame(original, modified);
```

这使得它在多线程环境中可以安全并发使用。

## Thread Safety

The library uses an immutable pattern:

```csharp
// Original instance remains unchanged
var original = new HtmlP();
var modified = original.Add("Hello");

// Both instances are independent
Assert.Equal("<p></p>", original.ToHtml());
Assert.Equal("<p>Hello</p>", modified.ToHtml());
Assert.NotSame(original, modified);
```

This makes it safe for concurrent use across multiple threads.

## 性能特性

- **对象池**：重用 List 对象以减少 GC 压力
- **StringBuilder 缓存**：线程本地缓存以提高字符串构建效率
- **容量预估**：预先分配 StringBuilder 容量以避免重新分配

## Performance Features

- **Object Pooling**: Reuses List objects to reduce GC pressure
- **StringBuilder Cache**: Thread-local caching for efficient string building
- **Capacity Estimation**: Pre-allocates StringBuilder capacity to avoid reallocations

## 安全注意事项

1. **自动编码**：通过 `Add()` 方法添加的所有文本都会自动进行 HTML 编码
2. **XSS 防护**：用户输入会自动进行清理
3. **原始 HTML 警告**：仅在内容可信时使用 `AddRaw()`

## Security

1. **Automatic Encoding**: All text added via `Add()` is HTML encoded
2. **XSS Protection**: User input is automatically sanitized
3. **Raw HTML Warning**: Use `AddRaw()` only with trusted content

## 支持的框架

- .NET Standard 2.0
- .NET 6
- .NET 7
- .NET 8
- .NET 9
- .NET 10

## Supported Frameworks

- .NET Standard 2.0
- .NET 6
- .NET 7
- .NET 8
- .NET 9
- .NET 10

## 许可证

MIT License

## License

MIT License

## 版本历史

### 1.0.0
- 初始版本，包含 HtmlP、HtmlSpan、HtmlFormatHelper
- 支持条件渲染和批量操作
- 线程安全的不可变模式
- 对象池优化性能
- StringBuilder 缓存
- 增强测试覆盖（91 个测试）
- 代码清理和重构

## Version History

### 1.0.0
- Initial release with HtmlP, HtmlSpan, HtmlFormatHelper
- Support for conditional rendering and batch operations
- Thread-safe immutable pattern
- Object pooling for performance
- StringBuilder caching
- Enhanced test coverage (91 tests)
- Code cleanup and refactoring
