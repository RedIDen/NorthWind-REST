using Microsoft.AspNetCore.Mvc;
using Northwind.Services.BloggingModels;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class CommentsController : Controller
    {
        private readonly ICommentingService _service;
        private readonly IBloggingService _bloggingService;

        public CommentsController(ICommentingService service, IBloggingService bloggingService)
            => (_service, _bloggingService) = (service, bloggingService);

        // POST api/articles/1/comments
        [HttpPost("{article_id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(int article_id, BlogComment comment)
        {
            if (!_bloggingService.TryShowArticle(article_id, out _))
            {
                return BadRequest();
            }

            comment.ArticleId = article_id;

            int? commentId = await _service.CreateCommentAsync(comment);

            if (commentId is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateAsync), new { id = commentId }, comment);
        }

        // GET api/articles/1/comments
        [HttpGet("{article_id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BlogCommentsShow>>> GetAsync(int article_id)
        {
            if (!_bloggingService.TryShowArticle(article_id, out BlogArticle article))
            {
                return BadRequest();
            }

            var comments = await _service.ShowCommentsAsync(0, int.MaxValue);
            var ans = comments.Where(comment => comment.ArticleId == article_id).Select(comment => GetComment(comment, article.Title!)).ToList();

            return ans;

            BlogCommentsShow GetComment(BlogComment comment, string articleTitle)
                => new()
                {
                    Id = comment.Id,
                    PublisherID = comment.PublisherID,
                    ArticleId = comment.ArticleId,
                    ArticleName = articleTitle,
                    Comment = comment.Comment,
                };
        }

        // DELETE api/articles/1/comments/1
        [HttpDelete("{article_id}/comments/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int article_id, int id)
        {
            if (!_bloggingService.TryShowArticle(article_id, out _))
            {
                return BadRequest();
            }

            var comments = await _service.ShowCommentsAsync(0, int.MaxValue);
            var comment = comments.Where(comment => comment.ArticleId == article_id && comment.Id == id).FirstOrDefault();

            await _service.DestroyCommentAsync(comment!.Id);

            return NoContent();
        }


        // PUT api/articles/1/comments/1
        [HttpPut("{article_id}/comments/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(int article_id, int id, BlogComment comment)
        {
            if (!_bloggingService.TryShowArticle(article_id, out _))
            {
                return BadRequest();
            }

            var comments = await _service.ShowCommentsAsync(0, int.MaxValue);
            var com = comments.Where(_comment => _comment.ArticleId == article_id).ToList()[id - 1];

            com.Comment = comment.Comment;

            await _service.UpdateCommentAsync(com.Id, com);

            return NoContent();
        }
    }
}
