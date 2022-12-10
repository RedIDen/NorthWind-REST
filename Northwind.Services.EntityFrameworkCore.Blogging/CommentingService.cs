using Northwind.Services.BloggingModels;
using Northwind.Services.EntityFrameworkCore.Blogging.Context;

namespace Northwind.Services.EntityFrameworkCore.Blogging
{
    public class CommentingService : ICommentingService
    {
        private readonly BloggingContext? _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentingService"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public CommentingService(string[] args)
            => _context = new DesignTimeBloggingContextFactory().CreateDbContext(args);

        public async Task<int> CreateCommentAsync(BlogComment comment)
        {
            _ = comment is null ? throw new ArgumentNullException(nameof(comment)) : comment;

            var result = await _context.BlogComments.AddAsync(GetCommentEnt(comment));
            await _context.SaveChangesAsync();

            return result.CurrentValues.GetValue<int>("Id");
        }

        public async Task<bool> DestroyCommentAsync(int commentId)
        {
            var comment = await _context.BlogComments.FindAsync(commentId);

            if (comment is null)
            {
                return false;
            }

            _context.BlogComments.Remove(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IList<BlogComment>> ShowCommentsAsync(int offset, int limit)
            => _context.BlogComments.Skip(offset).Take(limit).Select(comment => GetCommentMod(comment)).ToList();

        public async Task<bool> UpdateCommentAsync(int commentId, BlogComment comment)
        {
            var com = await _context.BlogComments.FindAsync(commentId);

            com.ArticleId = comment.ArticleId;
            com.Comment = comment.Comment;

            await _context.SaveChangesAsync();

            if (_context.BlogComments.Contains(GetCommentEnt(comment)))
            {
                return true;
            }

            return false;
        }

        public bool TryShowComments(int commentId, out BlogComment comment)
        {
            comment = GetCommentMod(_context.BlogComments.Find(commentId));

            if (comment is null)
            {
                return false;
            }

            return true;
        }

        private static BlogComment? GetCommentMod(Entities.BlogComment comment)
            => comment is null ? null : new ()
            {
                Id = comment.Id,
                PublisherID = comment.PublisherID,
                ArticleId = comment.ArticleId,
                Comment = comment.Comment,
            };

        private static Entities.BlogComment? GetCommentEnt(BlogComment comment)
            => comment is null ? null : new ()
            {
                Id = comment.Id,
                PublisherID = comment.PublisherID,
                ArticleId = comment.ArticleId,
                Comment = comment.Comment,
            };
    }
}
