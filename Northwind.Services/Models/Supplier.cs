namespace Northwind.Services.Models
{
    /// <summary>
    /// Represents a supplier.
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Gets or sets a suplier identifier.
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets a suplier company name.
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// Gets or sets a suplier contact name.
        /// </summary>
        public string? ContactName { get; set; }

        /// <summary>
        /// Gets or sets a suplier contact title.
        /// </summary>
        public string? ContactTitle { get; set; }

        /// <summary>
        /// Gets or sets a suplier address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets a suplier city.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets a suplier region.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets a suplier postal code.
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets a suplier conutry.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets a suplier phone.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets a suplier fax.
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// Gets or sets a suplier home page.
        /// </summary>
        public string? HomePage { get; set; }
    }
}
