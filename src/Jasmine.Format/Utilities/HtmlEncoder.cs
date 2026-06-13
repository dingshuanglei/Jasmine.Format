using System.Text;

namespace Jasmine.Format.Utilities
{
    /// <summary>
    /// HTML Encoder Utility Class
    /// </summary>
    public class HtmlEncoder
    {
        /// <summary>
        /// Encodes the input string for HTML to prevent XSS attacks.
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <returns>The encoded string.</returns>
        public static string HtmlEncode(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            StringBuilder sb = new StringBuilder(input.Length);
            foreach (char c in input)
            {
                switch (c)
                {
                    case '<': sb.Append("&lt;"); break;
                    case '>': sb.Append("&gt;"); break;
                    case '&': sb.Append("&amp;"); break;
                    case '"': sb.Append("&quot;"); break;
                    case '\'': sb.Append("&#39;"); break;
                    default: sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
    }
}