using Microsoft.AspNetCore.Mvc;
using Northwind.Services;
using Northwind.Services.Models;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryPicturesManagementService _pictureService;
        private readonly IProductCategoryManagementService _categoryService;

        public ProductCategoriesController(IProductCategoryPicturesManagementService pictureService, IProductCategoryManagementService categoryService)
            => (_pictureService, _categoryService) = (pictureService, categoryService);

        // POST api/categories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(ProductCategory category)
        {
            if (await _categoryService.CreateCategoryAsync(category) == -1)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateAsync), new { id = category.Id }, category);
        }

        // GET api/categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllAsync()
        {
            var category = await _categoryService.ShowCategoriesAsync(0, int.MaxValue) as List<ProductCategory>;

            if (category is null)
            {
                return BadRequest();
            }

            return category;
        }

        // GET api/categories/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductCategory>> GetAsync(int id)
        {
            if (!_categoryService.TryShowCategory(id, out ProductCategory category))
            {
                return NotFound();
            }

            return category;
        }

        // PUT api/categories/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, ProductCategory category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (!_categoryService.TryShowCategory(id, out _))
            {
                return BadRequest();
            }

            await _categoryService.UpdateCategoriesAsync(id, category);

            return NoContent();
        }

        // DELETE api/categories/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!_categoryService.TryShowCategory(id, out _))
            {
                return BadRequest();
            }

            await _categoryService.DestroyCategoryAsync(id);

            return NoContent();
        }

        // PUT api/categories/1/picture
        [HttpPut("{id}/picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePictureAsync(int id, IFormFile file)
        {
            var size = file.Length;

            if (file.Length > 0)
            {
                await _pictureService.UpdatePictureAsync(id, file.OpenReadStream());
            }

            return Ok(new { size });
        }

        // GET api/categories/1/picture
        [HttpGet("{id}/picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Stream> GetPicture(int id)
        {
            if(_pictureService.TryShowPicture(id, out byte[] picture))
            {
                return this.File(picture[78..], "image/jpg");
            }

            return BadRequest(); 
        }

        // DELETE api/categories/1/picture
        [HttpDelete("{id}/picture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePictureAsync(int id)
        {
            if (!_categoryService.TryShowCategory(id, out _))
            {
                return BadRequest();
            }

            await _pictureService.DestroyPictureAsync(id);

            return NoContent();
        }
    }
}
