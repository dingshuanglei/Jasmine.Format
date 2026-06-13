using Jasmine.Format.Utilities;
using System.Text;

namespace Jasmine.Format.Elements.Media
{
    /// <summary>
    /// Represents an HTML audio element.
    /// </summary>
    public class HtmlAudio : HtmlStyledElement
    {
        /// <summary>
        /// Gets the source URL of the audio file.
        /// </summary>
        public string Src { get; }

        /// <summary>
        /// Gets a value indicating whether the audio controls should be displayed.
        /// </summary>
        public bool Controls { get; }

        /// <summary>
        /// Gets a value indicating whether the audio should start playing automatically.
        /// </summary>
        public bool Autoplay { get; }

        /// <summary>
        /// Gets a value indicating whether the audio should loop continuously.
        /// </summary>
        public bool Loop { get; }

        /// <summary>
        /// Gets a value indicating whether the audio should be muted.
        /// </summary>
        public bool Muted { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAudio"/> class.
        /// </summary>
        /// <param name="src">The source URL of the audio file.</param>
        /// <param name="controls">Whether to display audio controls. Default is true.</param>
        /// <param name="autoplay">Whether to autoplay the audio. Default is false.</param>
        /// <param name="loop">Whether to loop the audio. Default is false.</param>
        /// <param name="muted">Whether the audio should be muted. Default is false.</param>
        /// <param name="style">Optional CSS style for the audio element.</param>
        public HtmlAudio(string src, bool controls = true, bool autoplay = false, bool loop = false, bool muted = false, string style = null)
        {
            Src = src ?? string.Empty;
            Controls = controls;
            Autoplay = autoplay;
            Loop = loop;
            Muted = muted;
            Style = style;
        }

        /// <summary>
        /// Converts the audio element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the audio element.</returns>
        public override string ToHtml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"<{HtmlTagNames.Audio}");

            if (Controls)
                builder.Append(" controls");
            if (Autoplay)
                builder.Append(" autoplay");
            if (Loop)
                builder.Append(" loop");
            if (Muted)
                builder.Append(" muted");
            if (!string.IsNullOrWhiteSpace(Style))
                builder.Append($" style=\"{HtmlEncoder.HtmlEncode(Style)}\"");

            builder.Append($"><{HtmlTagNames.Source} src=\"{HtmlEncoder.HtmlEncode(Src)}\" type=\"audio/mpeg\">");
            builder.Append($"</{HtmlTagNames.Audio}>");
            return builder.ToString();
        }
    }
}