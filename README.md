# Jasmine.Format

A .NET library for generating HTML content with a fluent API.

## Features

- **HTML Encoding**: Automatic XSS protection
- **Fluent API**: Chainable method calls
- **Conditional Rendering**: Dynamic content based on conditions
- **Batch Operations**: Process collections efficiently
- **Multi-framework Support**: .NET Standard 2.0, .NET 6/7/8/9/10

## Quick Start

```csharp
using Jasmine.Format;

var html = new HtmlP()
    .Add("Hello, ")
    .AddSpan("World", "#0066cc", "font-weight: bold;")
    .ToHtml();

// Output: <p>Hello, <span style="color:#0066cc;font-weight: bold;">World</span></p>
```

## Installation

```bash
dotnet add package Jasmine.Format
```

## Documentation

See [src/Jasmine.Format/README.md](src/Jasmine.Format/README.md) for full documentation and API reference.

## License

MIT License