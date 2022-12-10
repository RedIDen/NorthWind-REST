namespace Northwind.DataAccess.Products
{
    /// <summary>
    /// Represents a DAO for Northwind product categories.
    /// </summary>
    public interface IProductCategoryDataAccessObject
    {
        /// <summary>
        /// Inserts a new Northwind product category to a data storage.
        /// </summary>
        /// <param name="productCategory">A <see cref="ProductCategoryTransferObject"/>.</param>
        /// <returns>A data storage identifier of a new product category.</returns>
        Task<int> InsertProductCategoryAsync(ProductCategoryTransferObject productCategory);

        /// <summary>
        /// Deletes a Northwind product category from a data storage.
        /// </summary>
        /// <param name="productCategoryId">An product category identifier.</param>
        /// <returns>True if a product category is deleted; otherwise false.</returns>
        Task<bool> DeleteProductCategoryAsync(int productCategoryId);

        /// <summary>
        /// Updates a Northwind product category in a data storage.
        /// </summary>
        /// <param name="productCategory">A <see cref="ProductCategoryTransferObject"/>.</param>
        /// <returns>True if a product category is updated; otherwise false.</returns>
        Task<bool> UpdateProductCategoryAsync(ProductCategoryTransferObject productCategory);

        /// <summary>
        /// Finds a Northwind product category using a specified identifier.
        /// </summary>
        /// <param name="productCategoryId">A data storage identifier of an existed product category.</param>
        /// <returns>A <see cref="ProductCategoryTransferObject"/> with specified identifier.</returns>
        Task<ProductCategoryTransferObject> FindProductCategoryAsync(int productCategoryId);

        /// <summary>
        /// Selects the product categories asynchronous.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>A <see cref="Task{T}"/> of <see cref="IList{T}"/> of <see cref="ProductCategoryTransferObject"/>.</returns>
        Task<IList<ProductCategoryTransferObject>> SelectProductCategoriesAsync(int offset, int limit);

        /// <summary>
        /// Selects all Northwind product categories with specified names.
        /// </summary>
        /// <param name="productCategoryNames">A <see cref="ICollection{T}"/> of product category names.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="ProductCategoryTransferObject"/>.</returns>
        Task<IList<ProductCategoryTransferObject>> SelectProductCategoriesByNameAsync(ICollection<string> productCategoryNames);
    }
}