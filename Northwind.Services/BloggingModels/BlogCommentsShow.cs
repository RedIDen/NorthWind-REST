namespace Northwind.Services.BloggingModels
{
    /// <summary>
    /// BlogCommentsShow class.
    /// </summary>
    public class BlogCommentsShow
    {
        /// <summary>
        /// Gets or sets identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets publisher identifier.
        /// </summary>
        public int PublisherID { get; set; }

        /// <summary>
        /// Gets or sets article identifier.
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets article name.
        /// </summary>
        public string? ArticleName { get; set; }

        /// <summary>
        /// Gets or sets comment text.
        /// </summary>
        public string? Comment { get; set; }
    }
}
