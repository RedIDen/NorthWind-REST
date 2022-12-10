namespace Northwind.Services
{
    /// <summary>
    /// IProductCategoryPicturesService interface.
    /// </summary>
    public interface IProductCategoryPicturesManagementService
    {
        /// <summary>
        /// Try to show a product category picture.
        /// </summary>
        /// <param name="categoryId">A product category identifier.</param>
        /// <param name="bytes">An array of picture bytes.</param>
        /// <returns>True if a product category is exist; otherwise false.</returns>
        bool TryShowPicture(int categoryId, out byte[] bytes);

        /// <summary>
        /// Update a product category picture.
        /// </summary>
        /// <param name="categoryId">A product category identifier.</param>
        /// <param name="stream">A <see cref="Stream"/>.</param>
        /// <returns>True if a product category is exist; otherwise false.</returns>
        Task<bool> UpdatePictureAsync(int categoryId, Stream stream);

        /// <summary>
        /// Destroy a product category picture.
        /// </summary>
        /// <param name="categoryId">A product category identifier.</param>
        /// <returns>True if a product category is exist; otherwise false.</returns>
        Task<bool> DestroyPictureAsync(int categoryId);
    }
}
