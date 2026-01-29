namespace App.Modules.Base.Substrate.Models.Messages
{
    /// <summary>
    /// TODO: Describe
    /// </summary>
    public class SearchResponseItem
    {

        /// <summary>
        /// The name/type identifier of the Search Response Item record.
        /// </summary>
        public virtual string? TypeKey { get; set; }
        /// <summary>
        /// the record id for the Type found
        /// </summary>
        public virtual string? TypeId { get; set; }
        /// <summary>
        /// The display Title of the search result
        /// </summary>
        public virtual string? Title { get; set; }
        /// <summary>
        /// The display Description of the search item response
        /// </summary>
        public virtual string? Description { get; set; }
        /// <summary>
        /// The display image for the search item response
        /// </summary>
        public virtual string? ImageUrl { get; set; }
    }
}