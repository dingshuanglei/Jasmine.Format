using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Format.Utilities
{
    /// <summary>
    /// HTML Formatting Helper Class
    /// </summary>
    public static class HtmlFormatHelper
    {
        /// <summary>
        /// Formats HTML template with positional parameters.
        /// </summary>
        /// <param name="htmlTemplate">HTML template string with placeholders {0}, {1}, ...</param>
        /// <param name="args">Array of arguments to replace placeholders.</param>
        /// <returns>Formatted HTML string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when htmlTemplate is null or empty.</exception>
        public static string Format(string htmlTemplate, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(htmlTemplate))
                throw new ArgumentNullException(nameof(htmlTemplate), "HTML template cannot be null or empty");

            var escapeArgs = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                escapeArgs[i] = HtmlEncoder.HtmlEncode(args[i]?.ToString() ?? string.Empty);
            }

            return string.Format(htmlTemplate, escapeArgs);
        }

        /// <summary>
        /// Formats HTML template with named parameters (HTML encoding enabled by default).
        /// </summary>
        /// <param name="template">HTML template string with placeholders {key}.</param>
        /// <param name="keyValues">Array of key-value pairs for replacing placeholders.</param>
        /// <returns>Formatted HTML string.</returns>
        public static string FormatTemplate(string template, params KeyValuePair<string, string>[] keyValues)
        {
            return FormatTemplate(template, true, keyValues);
        }

        /// <summary>
        /// Formats HTML template with named parameters.
        /// </summary>
        /// <param name="template">HTML template string with placeholders {key}.</param>
        /// <param name="escape">Whether to HTML encode values.</param>
        /// <param name="keyValues">Array of key-value pairs for replacing placeholders.</param>
        /// <returns>Formatted HTML string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when template is null or empty.</exception>
        public static string FormatTemplate(string template, bool escape, params KeyValuePair<string, string>[] keyValues)
        {
            if (string.IsNullOrWhiteSpace(template))
                throw new ArgumentNullException(nameof(template), "HTML template cannot be null or empty");

            if (keyValues == null || keyValues.Length == 0)
                return template;

            StringBuilder result = new StringBuilder(template);
            foreach (var kv in keyValues)
            {
                string placeholder = "{" + kv.Key + "}";
                string value = escape ? HtmlEncoder.HtmlEncode(kv.Value ?? string.Empty) : (kv.Value ?? string.Empty);
                result.Replace(placeholder, value);
            }

            return result.ToString();
        }

        /// <summary>
        /// Formats HTML template with Dictionary.
        /// </summary>
        /// <param name="template">HTML template string with placeholders {key}.</param>
        /// <param name="keyValues">Dictionary for replacing placeholders.</param>
        /// <param name="escape">Whether to HTML encode values. Default is true.</param>
        /// <returns>Formatted HTML string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when template is null or empty.</exception>
        public static string FormatTemplate(string template, Dictionary<string, string> keyValues, bool escape = true)
        {
            if (string.IsNullOrWhiteSpace(template))
                throw new ArgumentNullException(nameof(template), "HTML template cannot be null or empty");

            if (keyValues == null || keyValues.Count == 0)
                return template;

            StringBuilder result = new StringBuilder(template);
            foreach (var kv in keyValues)
            {
                string placeholder = "{" + kv.Key + "}";
                string value = escape ? HtmlEncoder.HtmlEncode(kv.Value ?? string.Empty) : (kv.Value ?? string.Empty);
                result.Replace(placeholder, value);
            }

            return result.ToString();
        }
    }
}