using Northwind.DataAccess;
using Northwind.DataAccess.Products;
using Northwind.Services.Models;

namespace Northwind.Services.DataAccess
{
    /// <summary>
    /// ProductCategoriesManagementDataAccessService class.
    /// </summary>
    /// <seealso cref="Northwind.Services.IProductCategoryManagementService" />
    public class ProductCategoriesManagementDataAccessService : IProductCategoryManagementService
    {
        private readonly NorthwindDataAccessFactory? _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoriesManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ProductCategoriesManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

        /// <inheritdoc/>
        public async Task<int> CreateCategoryAsync(ProductCategory productCategory)
        {
            _ = productCategory is null ? throw new ArgumentNullException($"{nameof(productCategory)} is null") : productCategory;

            var category = new ProductCategoryTransferObject
            {
                Name = productCategory.Name,
                Description = productCategory.Description,
                Picture = productCategory.Picture,
            };

            return await _factory.GetProductCategoryDataAccessObject().InsertProductCategoryAsync(category);
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyCategoryAsync(int categoryId)
        {
            var access = _factory.GetProductCategoryDataAccessObject();

            try
            {
                var category = await access.FindProductCategoryAsync(categoryId);

                if (category is null)
                {
                    return false;
                }

                await access.DeleteProductCategoryAsync(categoryId);

                return true;
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<IList<ProductCategory>> ShowCategoriesAsync(int offset, int limit)
        {
            var list = await _factory.GetProductCategoryDataAccessObject().SelectProductCategoriesAsync(offset, limit);

            return list.Select(category =>
                {
                    return new ProductCategory
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        Picture = category.Picture,
                    };
                }).ToList();
        }

        /// <inheritdoc/>
        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            try
            {
                var category = _factory.GetProductCategoryDataAccessObject().FindProductCategoryAsync(categoryId);

                if (category is null)
                {
                    productCategory = null;

                    return false;
                }
                category.Wait();
                var cat = category.Result;

                productCategory = new ProductCategory
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Description = cat.Description,
                    Picture = cat.Picture,
                };

                return true;
            }
            catch (ProductCategoryNotFoundException)
            {
                productCategory = null;
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateCategoriesAsync(int categoryId, ProductCategory productCategory)
        {
            var category = new ProductCategoryTransferObject
            {
                Name = productCategory.Name,
                Description = productCategory.Description,
                Picture = productCategory.Picture,
            };

            return await _factory.GetProductCategoryDataAccessObject().UpdateProductCategoryAsync(category);
        }
    }
}
