using Northwind.Services.Models;

namespace Northwind.Services.BloggingModels
{
    /// <summary>
    /// BlogArticleProductShow class.
    /// </summary>
    public class BlogArticleProductShow
    {
        /// <summary>
        /// Gets or sets blog article name.
        /// </summary>
        public string BlogArticleName { get; set; }

        /// <summary>
        /// Gets or sets array of products.
        /// </summary>
        public Product[] Products { get; set; }
    }
}
