using Northwind.DataAccess;
using Northwind.DataAccess.Products;
using Northwind.Services.Models;

namespace Northwind.Services.DataAccess
{
    /// <summary>
    /// ProductManagementDataAccessService class.
    /// </summary>
    /// <seealso cref="Northwind.Services.IProductManagementService" />
    public class ProductManagementDataAccessService : IProductManagementService
    {
        private readonly NorthwindDataAccessFactory? _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ProductManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

        /// <inheritdoc/>
        public async Task<int> CreateProductAsync(Product product)
        {
            _ = product is null ? throw new ArgumentNullException($"{nameof(product)} is null") : product;

            var pro = new ProductTransferObject
            {
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            return await _factory.GetProductDataAccessObject().InsertProductAsync(pro);
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyProductAsync(int productId)
        {
            var access = _factory.GetProductDataAccessObject();

            try
            {
                var product = await access.FindProductAsync(productId);

                await access.DeleteProductAsync(productId);

                return true;
            }
            catch (ProductNotFoundException)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<IList<ProductCategory>> LookupCategoriesByNameAsync(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<Product>> LookupProductsByNameAsync(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<Product>> ShowProductsAsync(int offset, int limit)
        {
            var list = await _factory.GetProductDataAccessObject().SelectProductsAsync(offset, limit);

            return list.Select(product =>
            {
                return new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    SupplierId = product.SupplierId,
                    CategoryId = product.CategoryId,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                };
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<IList<Product>> ShowProductsForCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Product product)
        {
            try
            {
                var prod = _factory.GetProductDataAccessObject().FindProductAsync(productId);
                prod.Wait();
                var pro = prod.Result;

                if (prod is null)
                {
                    product = null;

                    return false;
                }

                product = new Product
                {
                    Id = pro.Id,
                    Name = pro.Name,
                    SupplierId = pro.SupplierId,
                    CategoryId = pro.CategoryId,
                    QuantityPerUnit = pro.QuantityPerUnit,
                    UnitPrice = pro.UnitPrice,
                    UnitsInStock = pro.UnitsInStock,
                    UnitsOnOrder = pro.UnitsOnOrder,
                    ReorderLevel = pro.ReorderLevel,
                    Discontinued = pro.Discontinued,
                };

                return true;
            }
            catch (ProductNotFoundException)
            {
                product = null;
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProductAsync(int productId, Product product)
        {
            var pro = new ProductTransferObject
            {
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            return await _factory.GetProductDataAccessObject().UpdateProductAsync(pro);
        }
    }
}