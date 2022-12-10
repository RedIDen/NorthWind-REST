using Northwind.DataAccess;
using Northwind.DataAccess.Products;

namespace Northwind.Services.DataAccess
{
    /// <summary>
    /// ProductCategoryPicturesManagementDataAccessService class.
    /// </summary>
    /// <seealso cref="Northwind.Services.IProductCategoryPicturesManagementService" />
    public class ProductCategoryPicturesManagementDataAccessService : IProductCategoryPicturesManagementService
    {
        private readonly NorthwindDataAccessFactory? _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryPicturesManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ProductCategoryPicturesManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

        /// <inheritdoc/>
        public async Task<bool> DestroyPictureAsync(int categoryId)
        {
            var access = _factory.GetProductCategoryDataAccessObject();

            var category = await access.FindProductCategoryAsync(categoryId);

            if (category is null)
            {
                return false;
            }

            category.Picture = null;

            await access.UpdateProductCategoryAsync(category);

            return true;
        }

        /// <inheritdoc/>
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            var access = _factory.GetProductCategoryDataAccessObject();

            try
            {
                var category = access.FindProductCategoryAsync(categoryId);
                category.Wait();
                bytes = category.Result.Picture;
                return true;
            }
            catch (ProductCategoryNotFoundException)
            {
                bytes = null;
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePictureAsync(int categoryId, Stream stream)
        {
            var access = _factory.GetProductCategoryDataAccessObject();

            try
            {
                var category = await access.FindProductCategoryAsync(categoryId);

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    if (memoryStream.Length < 2097152)
                    {
                        category.Picture = memoryStream.ToArray();
                        await access.UpdateProductCategoryAsync(category);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }
        }
    }
}
