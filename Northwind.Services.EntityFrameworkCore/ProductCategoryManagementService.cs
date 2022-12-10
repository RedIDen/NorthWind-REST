using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.EntityFrameworkCore.Entities;
using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <inheritdoc/>
    public class ProductCategoryManagementService : IProductCategoryManagementService
    {
        private readonly NorthwindContext? _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryManagementService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductCategoryManagementService(NorthwindContext context)
            => _context = context;

        /// <inheritdoc/>
        public async Task<int> CreateCategoryAsync(ProductCategory productCategory)
        {
            _ = productCategory is null ? throw new ArgumentNullException($"{nameof(productCategory)} is null") : productCategory;

            await _context.Categories.AddAsync(GetCategory(productCategory));
            await _context.SaveChangesAsync();

            return productCategory.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category is null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IList<ProductCategory>> ShowCategoriesAsync(int offset, int limit)
        {
            var list = limit != -1 ? _context.Categories.Skip(offset).Take(limit).Select(category => GetProductCategory(category)).ToList() : _context.Categories.Skip(offset).Select(category => GetProductCategory(category)).ToList();
            
            return list;
        }

        /// <inheritdoc/>
        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            productCategory = GetProductCategory(_context.Categories.Find(categoryId));

            if (productCategory is null)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateCategoriesAsync(int categoryId, ProductCategory productCategory)
        {
            var category = _context.Categories
                .Where(c => c.CategoryId == categoryId)
                .FirstOrDefault();

            category.CategoryName = productCategory.Name;
            category.Description = productCategory.Description;

            await _context.SaveChangesAsync();

            if (_context.Categories.Contains(GetCategory(productCategory)))
            {
                return true;
            }

            return false;
        }

        private static Category GetCategory(ProductCategory category)
            => new()
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                Description = category.Description,
                Picture = category.Picture,
            };

        private static ProductCategory GetProductCategory(Category category)
            => new()
            {
                Id = category.CategoryId,
                Name = category.CategoryName,
                Description = category.Description,
                Picture = category.Picture,
            };
    }
}
