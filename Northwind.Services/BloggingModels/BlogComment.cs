namespace Northwind.Services.BloggingModels
{
    /// <summary>
    /// Blog comment class.
    /// </summary>
    public class BlogComment
    {
        /// <summary>
        /// Gets or sets blog comment identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets article id.
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets publisher id.
        /// </summary>
        public int PublisherID { get; set; }

        /// <summary>
        /// Gets or sets comment text.
        /// </summary>
        public string? Comment { get; set; }
    }
}
