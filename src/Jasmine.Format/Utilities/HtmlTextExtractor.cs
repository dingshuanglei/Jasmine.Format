using System.Text;

namespace Jasmine.Format.Utilities
{
    /// <summary>
    /// HTML text extraction utility class for stripping HTML tags and extracting plain text.
    /// </summary>
    public static class HtmlTextExtractor
    {
        /// <summary>
        /// Maximum length of an HTML entity (e.g., &amp; = 5, &#x27; = 6).
        /// </summary>
        private const int MaxEntityLength = 10;

        /// <summary>
        /// Extracts plain text from HTML content by removing all HTML tags and decoding HTML entities.
        /// </summary>
        /// <param name="html">The HTML string to process.</param>
        /// <returns>Plain text content without any HTML tags.</returns>
        public static string ExtractPlainText(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            StringBuilder result = new StringBuilder(html.Length);
            bool insideTag = false;
            bool insideScript = false;
            bool insideStyle = false;

            for (int i = 0; i < html.Length; i++)
            {
                char currentChar = html[i];

                // Handle script/style content skipping
                if (insideScript || insideStyle)
                {
                    if (TrySkipSpecialTagContent(html, i, insideScript ? "script" : "style"))
                    {
                        insideScript = false;
                        insideStyle = false;
                        int tagEnd = html.IndexOf('>', i);
                        if (tagEnd > 0)
                        {
                            i = tagEnd;
                        }
                    }
                    continue;
                }

                // Handle tag boundaries
                if (currentChar == '<')
                {
                    insideTag = true;
                    if (TryDetectSpecialTag(html, i, "script"))
                    {
                        insideScript = true;
                    }
                    else if (TryDetectSpecialTag(html, i, "style"))
                    {
                        insideStyle = true;
                    }
                    continue;
                }

                if (currentChar == '>')
                {
                    insideTag = false;
                    continue;
                }

                // Extract text content outside tags
                if (!insideTag)
                {
                    // Decode HTML entities
                    if (currentChar == '&')
                    {
                        string decoded = DecodeHtmlEntity(html, i);
                        if (decoded != null)
                        {
                            result.Append(decoded);
                            // Skip the entity
                            int entityEnd = html.IndexOf(';', i);
                            if (entityEnd > 0)
                            {
                                i = entityEnd;
                            }
                            continue;
                        }
                    }
                    result.Append(currentChar);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Attempts to detect a special tag (script or style) at the given position.
        /// </summary>
        private static bool TryDetectSpecialTag(string html, int startIndex, string tagName)
        {
            int tagEnd = html.IndexOf('>', startIndex);
            if (tagEnd <= startIndex)
                return false;

            string tagContent = html.Substring(startIndex + 1, tagEnd - startIndex - 1).Trim().ToLowerInvariant();
            return tagContent.StartsWith(tagName);
        }

        /// <summary>
        /// Attempts to skip content inside special tags until the closing tag is found.
        /// </summary>
        private static bool TrySkipSpecialTagContent(string html, int startIndex, string tagName)
        {
            if (html[startIndex] != '<' || startIndex + 1 >= html.Length || html[startIndex + 1] != '/')
                return false;

            int tagEnd = html.IndexOf('>', startIndex);
            if (tagEnd <= startIndex)
                return false;

            string closingTag = html.Substring(startIndex, tagEnd - startIndex + 1).ToLowerInvariant();
            return closingTag.Contains(tagName);
        }

        /// <summary>
        /// Decodes common HTML entities.
        /// </summary>
        private static string DecodeHtmlEntity(string html, int startIndex)
        {
            if (startIndex >= html.Length - 1)
                return null;

            int entityEnd = html.IndexOf(';', startIndex);
            if (entityEnd <= startIndex || entityEnd - startIndex > MaxEntityLength)
                return null;

            string entity = html.Substring(startIndex, entityEnd - startIndex + 1).ToLowerInvariant();

            switch (entity)
            {
                case "&lt;": return "<";
                case "&gt;": return ">";
                case "&amp;": return "&";
                case "&quot;": return "\"";
                case "&apos;": return "'";
                case "&nbsp;": return " ";
                case "&copy;": return "©";
                case "&reg;": return "®";
                case "&trade;": return "™";
                default:
                    // Handle numeric entities like &#39; or &#x27;
                    if (entity.StartsWith("&#"))
                    {
                        try
                        {
                            if (entity.StartsWith("&#x"))
                            {
                                // Hexadecimal entity
                                string hexValue = entity.Substring(3, entity.Length - 4);
                                int charCode = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
                                return char.ToString((char)charCode);
                            }
                            else
                            {
                                // Decimal entity
                                string decValue = entity.Substring(2, entity.Length - 3);
                                int charCode = int.Parse(decValue);
                                return char.ToString((char)charCode);
                            }
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    return null;
            }
        }
    }
}