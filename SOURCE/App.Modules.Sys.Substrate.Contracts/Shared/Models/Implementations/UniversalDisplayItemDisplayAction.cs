
namespace App.Modules.Sys.Shared.Models.Implementations
{

        /// <summary>
        /// An implementation of <see cref="IUniversalDisplayItemDisplayAction"/>
        /// </summary>
        public class UniversalDisplayItemDisplayAction : IUniversalDisplayItemDisplayAction
    {
        /// <inheritdoc/>
        public string ActionKey { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Title { get; set; } = string.Empty;
        /// <inheritdoc/>
        public string Description { get; set; } = string.Empty;



        /// <inheritdoc/>
        public string? ImageUrl { get; set; }

        /// <inheritdoc/>
        public string? ImageStyle { get; set; }


        /// <inheritdoc/>
        public IDictionary<string, object> Metadata => new Dictionary<string, object>();

    }
}

