namespace Northwind.Services.BloggingModels
{
    public interface ICommentingService
    {
        Task<int> CreateCommentAsync(BlogComment comment);
        bool TryShowComments(int commentId, out BlogComment comment);
        Task<IList<BlogComment>> ShowCommentsAsync(int offset, int limit);
        Task<bool> DestroyCommentAsync(int commentId);
        Task<bool> UpdateCommentAsync(int commentId, BlogComment comment);
    }
}
