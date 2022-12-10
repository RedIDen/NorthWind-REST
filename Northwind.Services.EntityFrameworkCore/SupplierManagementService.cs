using Northwind.Services.EntityFrameworkCore.Context;

namespace Northwind.Services.EntityFrameworkCore
{
    public class SupplierManagementService : ISupplierManagementService
    {
        private readonly NorthwindContext? _context;

        public SupplierManagementService(NorthwindContext context)
            => _context = context;

        public async Task<IList<Models.Supplier>> ShowSuppliersAsync(int offset, int limit)
            => limit != -1 ? _context.Suppliers.Skip(offset).Take(limit).Select(supplier => GetSupplierMod(supplier)).ToList() : _context.Suppliers.Skip(offset).Select(supplier => GetSupplierMod(supplier)).ToList();

        /// <inheritdoc/>
        public bool TryShowProduct(int supplierId, out Models.Supplier supplier)
        {
            supplier = GetSupplierMod(_context.Suppliers.Find(supplierId));

            if (supplier is null)
            {
                return false;
            }

            return true;
        }

        private static Models.Supplier GetSupplierMod(Entities.Supplier supplier)
            => new()
            {
                SupplierId = supplier.SupplierId,
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                ContactTitle = supplier.ContactTitle,
                Address = supplier.Address,
                City = supplier.City,
                Region = supplier.Region,
                PostalCode = supplier.PostalCode,
                Country = supplier.Country,
                Phone = supplier.Phone,
                Fax = supplier.Fax,
                HomePage = supplier.HomePage,
            };
    }
}
