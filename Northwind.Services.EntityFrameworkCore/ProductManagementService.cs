using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <summary>
    /// Represents a stub for a product management service.
    /// </summary>
    public sealed class ProductManagementService : IProductManagementService
    {
        private static NorthwindContext? _context;

        public ProductManagementService(NorthwindContext context)
            => _context = context;

        /// <inheritdoc/>
        public async Task<int> CreateProductAsync(Models.Product product)
        {
            _ = product is null ? throw new ArgumentNullException($"{nameof(product)} is null") : product;

            await _context.Products.AddAsync(GetProductEnt(product));
            await _context.SaveChangesAsync();

            return product.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product is null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IList<ProductCategory>> LookupCategoriesByNameAsync(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<Models.Product>> LookupProductsByNameAsync(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<Models.Product>> ShowProductsAsync(int offset, int limit)
            => limit != -1 ? _context.Products.Skip(offset).Take(limit).Select(product => GetProductMod(product)).ToList() : _context.Products.Skip(offset).Select(product => GetProductMod(product)).ToList();

        /// <inheritdoc/>
        public async Task<IList<Models.Product>> ShowProductsForCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Models.Product product)
        {
            product = GetProductMod(_context.Products.Find(productId));

            if (product is null)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProductAsync(int productId, Models.Product product)
        {
            var productObject = _context.Products
                .Where(p => p.ProductId == productId)
                .FirstOrDefault();

            productObject.ProductName = product.Name;
            productObject.SupplierId = product.SupplierId;
            productObject.UnitsOnOrder = product.UnitsOnOrder;
            productObject.UnitsInStock = product.UnitsInStock;
            productObject.QuantityPerUnit = product.QuantityPerUnit;
            productObject.UnitPrice = product.UnitPrice;
            productObject.Discontinued = product.Discontinued;
            productObject.CategoryId = product.CategoryId;
            productObject.ReorderLevel = product.ReorderLevel;


            await _context.SaveChangesAsync();

            if (_context.Products.Contains(GetProductEnt(product)))
            {
                return true;
            }

            return false;
        }

        private static Models.Product GetProductMod(Entities.Product product)
            => new()
            {
                Id = product.ProductId,
                Name = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

        private static Entities.Product GetProductEnt(Models.Product product)
            => new()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };
    }
}
