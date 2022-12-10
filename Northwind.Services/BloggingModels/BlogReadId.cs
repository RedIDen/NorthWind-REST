namespace Northwind.Services.BloggingModels
{
    public class BlogReadId
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the date published.
        /// </summary>
        /// <value>
        /// The date published.
        /// </value>
        public DateTime? DatePublished { get; set; }

        /// <summary>
        /// Gets or sets the publisher identifier.
        /// </summary>
        /// <value>
        /// The publisher identifier.
        /// </value>
        public int PublisherId { get; set; }

        /// <summary>
        /// Gets or sets the name of the publisher.
        /// </summary>
        /// <value>
        /// The name of the publisher.
        /// </value>
        public string PublisherName { get; set; }
    }
}
