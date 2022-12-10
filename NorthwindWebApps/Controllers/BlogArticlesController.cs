using Microsoft.AspNetCore.Mvc;
using Northwind.Services;
using Northwind.Services.BloggingModels;
using Northwind.Services.Models;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class BlogArticlesController : Controller
    {
        private readonly IBloggingService _service;
        private readonly IEmployeeManagementService _employeeManagementService;
        private readonly IProductManagementService _productManagementService;

        public BlogArticlesController(IBloggingService service, IEmployeeManagementService employeeManagementService, IProductManagementService productManagementService)
            => (_service, _employeeManagementService, _productManagementService) = (service, employeeManagementService, productManagementService);

        // POST api/articles
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(BlogArticle article)
        {
            article.DatePublished = DateTime.Now;

            if (!_employeeManagementService.TryShowEmployee(article.PublisherId, out _))
            {
                return BadRequest();
            }

            int? employeeId = await _service.CreateArticleAsync(article);

            if (employeeId is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateAsync), new { id = employeeId }, article);
        }

        // GET api/articles
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BlogRead>>> GetAllAsync()
        {
            var articles = await _service.ShowArticlesAsync(0, int.MaxValue) as List<BlogArticle>;

            if (articles is null)
            {
                return BadRequest();
            }

            var answer = new List<BlogRead>();

            foreach (var item in articles)
            {
                _employeeManagementService.TryShowEmployee(item.PublisherId, out Employee employee);

                var blog = new BlogRead()
                {
                    Id = item.Id,
                    Title = item.Title,
                    DatePublished = item.DatePublished,
                    PublisherId = item.PublisherId,
                    PublisherName = employee.FirstName + " " + employee.LastName + ", " + employee.Title,
                };

                answer.Add(blog);
            }

            return answer;
        }

        // GET api/articles/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BlogReadId>> GetAsync(int id)
        {
            if (!_service.TryShowArticle(id, out BlogArticle article))
            {
                return BadRequest();
            }

            _employeeManagementService.TryShowEmployee(article.PublisherId, out Employee employee);

            var blog = new BlogReadId()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
                PublisherName = employee.FirstName + " " + employee.LastName + ", " + employee.Title,
            };

            return blog;
        }

        // DELETE api/articles/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!_service.TryShowArticle(id, out _))
            {
                return BadRequest();
            }

            await _service.DestroyArticleAsync(id);

            return NoContent();
        }

        // PUT api/articles/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(int id, BlogArticle article)
        {
            if (!_service.TryShowArticle(id, out BlogArticle blog))
            {
                return BadRequest();
            }

            blog.Text = article.Text;
            blog.Title = article.Title;
            blog.PublisherId = article.PublisherId;
            blog.DatePublished = DateTime.Now;

            await _service.UpdateArticleAsync(id, blog);

            return NoContent();
        }

        // GET api/articles/1/products
        [HttpGet("{articleId}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlogArticleProductShow>> GetAllProductsInArticleAsync(int articleId)
        {
            var products = await _service.ShowProductsInArticleAsync(articleId, 0, int.MaxValue) as List<BlogArticleProduct>;

            if (products is null)
            {
                return BadRequest();
            }

            _service.TryShowArticle(articleId, out BlogArticle article);

            var productNames = new List<Product>();

            foreach (var item in products)
            {
                _productManagementService.TryShowProduct(item.ProductID, out Product product);

                productNames.Add(product);
            }

            var answer = new BlogArticleProductShow()
            {
                BlogArticleName = article.Title!,
                Products = productNames.ToArray(),
            };

            return answer;
        }

        // POST api/articles/1/products/1
        [HttpPost("{articleId}/products/{productId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateALinkToAProductAsync(int articleId, int productId)
        {
            var result = await _service.AddALinkToAProductAsync(articleId, productId);

            if (result is false)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateALinkToAProductAsync), articleId, productId);
        }

        // DELETE api/articles/1/products/1
        [HttpDelete("{articleId}/products/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteALinkToAProductAsyncAsync(int articleId, int productId)
        {
            if (!_service.TryShowArticle(articleId, out _))
            {
                return BadRequest();
            }

            await _service.DestroyExistedLinkToAProductAsync(articleId, productId);

            return NoContent();
        }
    }
}
