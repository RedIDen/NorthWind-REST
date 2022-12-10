using Northwind.Services.Models;

namespace Northwind.Services
{
    /// <summary>
    /// IProductCategoryManagementService interface.
    /// </summary>
    public interface IProductCategoryManagementService
    {
        /// <summary>
        /// Shows the categories asynchronous.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>A <see cref="Task{T}"/> of <see cref="IList{T}"/> of <see cref="ProductCategory"/>.</returns>
        Task<IList<ProductCategory>> ShowCategoriesAsync(int offset, int limit);

        /// <summary>
        /// Try to show a product category with specified identifier.
        /// </summary>
        /// <param name="categoryId">A product category identifier.</param>
        /// <param name="productCategory">A product category to return.</param>
        /// <returns>Returns true if a product category is returned; otherwise false.</returns>
        bool TryShowCategory(int categoryId, out ProductCategory productCategory);

        /// <summary>
        /// Creates a new product category.
        /// </summary>
        /// <param name="productCategory">A <see cref="ProductCategory"/> to create.</param>
        /// <returns>An identifier of a created product category.</returns>
        Task<int> CreateCategoryAsync(ProductCategory productCategory);

        /// <summary>
        /// Destroys an existed product category.
        /// </summary>
        /// <param name="categoryId">A product category identifier.</param>
        /// <returns>True if a product category is destroyed; otherwise false.</returns>
        Task<bool> DestroyCategoryAsync(int categoryId);

        /// <summary>
        /// Updates a product category.
        /// </summary>
        /// <param name="categoryId">A product category identifier.</param>
        /// <param name="productCategory">A <see cref="ProductCategory"/>.</param>
        /// <returns>True if a product category is updated; otherwise false.</returns>
        Task<bool> UpdateCategoriesAsync(int categoryId, ProductCategory productCategory);
    }
}
