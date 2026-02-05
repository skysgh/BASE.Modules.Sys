using App.Modules.Sys.Shared.Constants;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Implementations
{
    /// <summary>
    /// A single entry in the startup log.
    /// </summary>
    public class StartupLogEntry : UniversalDisplayItem, IHasStartAndEndUtcNullable, IHasException
    {

        /// <inheritdoc/>
        public DateTime? StartUtc { get; set; }
        /// <inheritdoc/>
        public DateTime? EndUtc { get; set; }
        

        /// <summary>
        /// the Calculated duration of the operation.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                if (StartUtc == null)
                {
                    return TimeSpan.Zero;
                }
                if (EndUtc == null)
                {
                    return TimeSpan.Zero;
                }
                return StartUtc.Value - EndUtc.Value;
            }
        }


        /// <inheritdoc/>
        public Exception? Exception { get; set; }


        /// <summary>
        /// Start the entry.
        /// </summary>
        /// <param name="title">Entry title.</param>
        /// <param name="description">Optional description.</param>
        /// <param name="tags">Optional tags for categorization.</param>
        public void Start(string title, string? description = null, params string[] tags)
        {
            Title = title;
            Description = description ?? string.Empty;
            Tags = tags ?? Array.Empty<string>();

            StartUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Start the entry with a single tag.
        /// </summary>
        public void StartWithTag(string title, string tag, string? description = null)
        {
            Start(title, description, new[] { tag });
        }

        /// <summary>
        /// Finalize the entry.
        /// </summary>
        public void FinalizeEntry()
        {
            EndUtc = DateTime.UtcNow;

            Metadata[MetadataKeys.Duration] = $"{Duration.TotalMilliseconds:F0}ms";
            if (Exception != null)
            {
                Metadata[MetadataKeys.ExceptionType] = Exception.GetType().Name;
                Metadata[MetadataKeys.ExceptionMessage] = Exception.Message;
                
                // Auto-add Error tag if exception occurred
                var tagsList = new List<string>(Tags);
                if (!tagsList.Contains(StartupTags.Error))
                {
                    tagsList.Add(StartupTags.Error);
                }
                Tags = tagsList;
            }
        }
    }
}