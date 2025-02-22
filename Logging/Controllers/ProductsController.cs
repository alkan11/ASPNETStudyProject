using Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
         private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = new List<Product>()
            {
                new Product(){Id=1,Name="Kitap",Title="K"},
                new Product(){Id=2,Name="Defter",Title="D"},
                new Product(){Id=3,Name="Cetvel",Title="C"},
            };
            _logger.LogInformation("GetAllProducts action has been called");
            return Ok(products);
        }
    }
}
