using Microsoft.AspNetCore.Mvc;
using Northwind.Services;
using Northwind.Services.Models;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierManagementService _service;

        public SuppliersController(ISupplierManagementService service)
            => _service = service;

        // GET api/suppliers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllAsync()
        {
            var suppliers = await _service.ShowSuppliersAsync(0, int.MaxValue) as List<Supplier>;

            if (suppliers is null)
            {
                return BadRequest();
            }

            return suppliers;
        }

        // GET api/suppliers/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Supplier>> GetAsync(int id)
        {
            if (!_service.TryShowProduct(id, out Supplier supplier))
            {
                return NotFound();
            }

            return supplier;
        }
    }
}
