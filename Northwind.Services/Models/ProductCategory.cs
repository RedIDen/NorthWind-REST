namespace Northwind.Services.Models
{
    /// <summary>
    /// Represents a product category.
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// Gets or sets a product category identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a product category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a product category description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        public byte[] Picture { get; set; }
    }
}
