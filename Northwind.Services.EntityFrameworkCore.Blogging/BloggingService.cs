using Northwind.Services.BloggingModels;
using Northwind.Services.EntityFrameworkCore.Blogging.Context;

namespace Northwind.Services.EntityFrameworkCore.Blogging
{
    /// <summary>
    /// BloggingService class.
    /// </summary>
    /// <seealso cref="Northwind.Services.BloggingModels.IBloggingService" />
    public class BloggingService : IBloggingService
    {
        private readonly BloggingContext? _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BloggingService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BloggingService(string[] args)
            => _context = new DesignTimeBloggingContextFactory().CreateDbContext(args);

        /// <inheritdoc/>
        public async Task<int> CreateArticleAsync(BlogArticle article)
        {
            _ = article is null ? throw new ArgumentNullException(nameof(article)) : article;

            var a = await _context.BlogArticles.AddAsync(GetArticleEnt(article));
            await _context.SaveChangesAsync();

            return a.CurrentValues.GetValue<int>("BlogArticleID");
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyArticleAsync(int articleId)
        {
            var article = await _context.BlogArticles.FindAsync(articleId);

            if (article is null)
            {
                return false;
            }

            _context.BlogArticles.Remove(article);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IList<BlogArticle>> ShowArticlesAsync(int offset, int limit)
            => limit != -1 ? _context.BlogArticles.Skip(offset).Take(limit).Select(product => GetArticleMod(product)).ToList() : _context.BlogArticles.Skip(offset).Select(product => GetArticleMod(product)).ToList();

        /// <inheritdoc/>
        public bool TryShowArticle(int articleId, out BlogArticle article)
        {
            article = GetArticleMod(_context.BlogArticles.Find(articleId));

            if (article is null)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateArticleAsync(int articleId, BlogArticle article)
        {
            var articleObject = await _context.BlogArticles.FindAsync(articleId);

            articleObject.Title = article.Title;
            articleObject.Text = article.Text;
            articleObject.DatePublished = article.DatePublished;
            articleObject.PublisherId = article.PublisherId;

            await _context.SaveChangesAsync();

            if (_context.BlogArticles.Contains(GetArticleEnt(article)))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public async Task<IList<BlogArticleProduct?>> ShowProductsInArticleAsync(int articleId, int offset, int limit)
            => _context.BlogArticleProducts
                                    .Where(articleProducts => articleProducts.BlogArticleID == articleId)
                                    .Skip(offset)
                                    .Take(limit)
                                    .Select(articleProducts => GetBlogArticleProductMod(articleProducts))
                                    .ToList();

        /// <inheritdoc/>
        public async Task<bool> AddALinkToAProductAsync(int articleId, int productId)
        {
            var blogArticleProduct = new BlogArticleProduct()
            {
                BlogArticleID = articleId,
                ProductID = productId,
            };

            var result = await _context.BlogArticleProducts.AddAsync(GetBlogArticleProductEnt(blogArticleProduct));
            await _context.SaveChangesAsync();

            return result is null ? false : true;
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyExistedLinkToAProductAsync(int articleId, int productId)
        {
            var blogArticleProduct = _context.BlogArticleProducts.Where(link => link.BlogArticleID == articleId && link.ProductID == productId).FirstOrDefault();

            var result = _context.BlogArticleProducts.Remove(blogArticleProduct);
            await _context.SaveChangesAsync();

            return result is null ? false : true;
        }

        private static BlogArticle? GetArticleMod(Entities.BlogArticle article)
            => article is null ? null : new ()
            {
                Id = article.BlogArticleID,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
            };

        private static Entities.BlogArticle? GetArticleEnt(BlogArticle article)
            => article is null ? null : new ()
            {
                BlogArticleID = article.Id,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
            };

        private static BlogArticleProduct? GetBlogArticleProductMod(Entities.BlogArticleProduct blogArticleProduct)
            => blogArticleProduct is null ? null : new ()
            {
                Id = blogArticleProduct.BlogArticleProductID,
                BlogArticleID = blogArticleProduct.BlogArticleID,
                ProductID = blogArticleProduct.ProductID,
            };

        private static Entities.BlogArticleProduct? GetBlogArticleProductEnt(BlogArticleProduct blogArticleProduct)
            => blogArticleProduct is null ? null : new ()
            {
                BlogArticleProductID = blogArticleProduct.Id,
                BlogArticleID = blogArticleProduct.BlogArticleID,
                ProductID = blogArticleProduct.ProductID,
            };
    }
}
