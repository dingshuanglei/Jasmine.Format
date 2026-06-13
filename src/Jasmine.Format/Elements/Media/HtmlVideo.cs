using Jasmine.Format.Utilities;
using System.Text;

namespace Jasmine.Format.Elements.Media
{
    /// <summary>
    /// Represents an HTML video element.
    /// </summary>
    public class HtmlVideo : HtmlStyledElement
    {
        /// <summary>
        /// Gets the source URL of the video.
        /// </summary>
        public string Src { get; }

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        public string Width { get; }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        public string Height { get; }

        /// <summary>
        /// Gets the poster image URL of the video.
        /// </summary>
        public string Poster { get; }

        /// <summary>
        /// Gets a value indicating whether video controls are displayed.
        /// </summary>
        public bool Controls { get; }

        /// <summary>
        /// Gets a value indicating whether video starts playing automatically.
        /// </summary>
        public bool Autoplay { get; }

        /// <summary>
        /// Gets a value indicating whether video loops continuously.
        /// </summary>
        public bool Loop { get; }

        /// <summary>
        /// Gets a value indicating whether video is muted.
        /// </summary>
        public bool Muted { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlVideo"/> class.
        /// </summary>
        /// <param name="src">The source URL of the video file (required).</param>
        /// <param name="width">The width of the video. Default is null.</param>
        /// <param name="height">The height of the video. Default is null.</param>
        /// <param name="poster">The poster image URL. Default is null.</param>
        /// <param name="controls">Whether to display video controls. Default is true.</param>
        /// <param name="autoplay">Whether to autoplay the video. Default is false.</param>
        /// <param name="loop">Whether to loop the video. Default is false.</param>
        /// <param name="muted">Whether to mute the video. Default is false.</param>
        /// <param name="style">Optional CSS style for the element. Default is null.</param>
        public HtmlVideo(string src, string width = null, string height = null, string poster = null,
                         bool controls = true, bool autoplay = false, bool loop = false, bool muted = false,
                         string style = null)
        {
            Src = src ?? string.Empty;
            Width = width;
            Height = height;
            Poster = poster;
            Controls = controls;
            Autoplay = autoplay;
            Loop = loop;
            Muted = muted;
            Style = style;
        }

        /// <summary>
        /// Creates a new HtmlVideo with the specified style.
        /// </summary>
        /// <param name="style">The CSS style to apply.</param>
        /// <returns>A new HtmlVideo instance with the specified style.</returns>
        public HtmlVideo WithStyle(string style)
        {
            return new HtmlVideo(Src, Width, Height, Poster, Controls, Autoplay, Loop, Muted, style);
        }

        /// <summary>
        /// Converts the video element to an HTML string.
        /// </summary>
        /// <returns>HTML string representation of the video element.</returns>
        public override string ToHtml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"<{HtmlTagNames.Video}");

            if (!string.IsNullOrWhiteSpace(Width))
                builder.Append($" width=\"{HtmlEncoder.HtmlEncode(Width)}\"");
            if (!string.IsNullOrWhiteSpace(Height))
                builder.Append($" height=\"{HtmlEncoder.HtmlEncode(Height)}\"");
            if (!string.IsNullOrWhiteSpace(Poster))
                builder.Append($" poster=\"{HtmlEncoder.HtmlEncode(Poster)}\"");
            if (!string.IsNullOrWhiteSpace(Style))
                builder.Append($" style=\"{HtmlEncoder.HtmlEncode(Style)}\"");
            if (Controls)
                builder.Append(" controls");
            if (Autoplay)
                builder.Append(" autoplay");
            if (Loop)
                builder.Append(" loop");
            if (Muted)
                builder.Append(" muted");

            builder.Append($"><{HtmlTagNames.Source} src=\"{HtmlEncoder.HtmlEncode(Src)}\" type=\"video/mp4\"></{HtmlTagNames.Video}>");
            return builder.ToString();
        }
    }
}