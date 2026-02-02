
using App.Modules.Sys.Shared.Models.Enums;

namespace App.Modules.Sys.Shared.Models.Implementations
{
    /// <summary>
    /// IMplementation of <see cref="IUniversalDisplayItem"/>.
    /// </summary>
    public class UniversalDisplayItem : IUniversalDisplayItem
    {

        /// <inheritdoc/>
        public TraceLevel Level { get; set; } = TraceLevel.Info;

        /// <inheritdoc/>
        public string Title { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Description { get; set; } = string.Empty;



        /// <inheritdoc/>
        public string? DisplayStyleHint { get; set; }


        /// <inheritdoc/>
        public string? ImageUrl { get; set; }

        /// <inheritdoc/>
        public string? ImageStyle { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IUniversalDisplayItemDisplayAction> AvailableActions {
            get;
            } = new  List<IUniversalDisplayItemDisplayAction>(); 

        /// <inheritdoc/>
        public IDictionary<string, object> Metadata => throw new NotImplementedException();
    }
}

