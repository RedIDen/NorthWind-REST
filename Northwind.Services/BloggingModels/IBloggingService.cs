namespace Northwind.Services.BloggingModels
{
    /// <summary>
    /// IBloggingService interface.
    /// </summary>
    public interface IBloggingService
    {
        /// <summary>
        /// Creates the article asynchronous.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns></returns>
        Task<int> CreateArticleAsync(BlogArticle article);

        /// <summary>
        /// Shows the articles asynchronous.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        Task<IList<BlogArticle>> ShowArticlesAsync(int offset, int limit);

        /// <summary>
        /// Tries the show article.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="article">The article.</param>
        /// <returns></returns>
        bool TryShowArticle(int articleId, out BlogArticle article);

        /// <summary>
        /// Destroys the article asynchronous.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <returns></returns>
        Task<bool> DestroyArticleAsync(int articleId);

        /// <summary>
        /// Updates the article asynchronous.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="article">The article.</param>
        /// <returns></returns>
        Task<bool> UpdateArticleAsync(int articleId, BlogArticle article);

        /// <summary>
        /// Shows the products in article asynchronous.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        Task<IList<BlogArticleProduct?>> ShowProductsInArticleAsync(int articleId, int offset, int limit);

        /// <summary>
        /// Creates a link to a product asynchronous.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task<bool> AddALinkToAProductAsync(int articleId, int productId);

        /// <summary>
        /// Removes the existed link to a product.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task<bool> DestroyExistedLinkToAProductAsync(int articleId, int productId);
    }
}
