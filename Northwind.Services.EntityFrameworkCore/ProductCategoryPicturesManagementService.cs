using Northwind.Services.EntityFrameworkCore.Context;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <inheritdoc/>
    public class ProductCategoryPicturesManagementService : IProductCategoryPicturesManagementService
    {
        private readonly NorthwindContext? _context;

        public ProductCategoryPicturesManagementService(NorthwindContext context)
            => _context = context;

        /// <inheritdoc/>
        public async Task<bool> DestroyPictureAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category is null)
            {
                return false;
            }

            category.Picture = null;
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            var category = _context.Categories.Find(categoryId);

            if (category is null)
            {
                bytes = null;
                return false;
            }

            bytes = category.Picture;
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePictureAsync(int categoryId, Stream stream)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category is null)
            {
                return false;
            }

            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);

                if (memoryStream.Length < 2097152)
                {
                    category.Picture = memoryStream.ToArray();

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
