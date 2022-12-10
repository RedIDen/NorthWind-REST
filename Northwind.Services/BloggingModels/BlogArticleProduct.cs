namespace Northwind.Services.BloggingModels
{
    /// <summary>
    /// BlogArticleProductClass. 
    /// </summary>
    public class BlogArticleProduct
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// Gets or sets the blog article identifier.
        /// </summary>
        /// <value>
        /// The blog article identifier.
        /// </value>
        public int BlogArticleID { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductID { get; set; }
    }
}
