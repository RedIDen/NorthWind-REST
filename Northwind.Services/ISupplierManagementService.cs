using Northwind.Services.Models;

namespace Northwind.Services
{
    public interface ISupplierManagementService
    {
        /// <summary>
        /// Shows a list of products using specified offset and limit for pagination.
        /// </summary>
        /// <param name="offset">An offset of the first element to return.</param>
        /// <param name="limit">A limit of elements to return.</param>
        /// <returns>A <see cref="IList{T}"/> of <see cref="Product"/>.</returns>
        Task<IList<Supplier>> ShowSuppliersAsync(int offset, int limit);

        public bool TryShowProduct(int supplierId, out Models.Supplier supplier);
    }
}
