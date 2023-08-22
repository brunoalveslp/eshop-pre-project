using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepo;
    public ProductsController(IGenericRepository<Product> productsRepo)
    {
        _productsRepo = productsRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandSpecification();    
        var products = await _productsRepo.ListAsync(spec);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandSpecification(id);
        var product = await _productsRepo.GetEntityWithSpecification(spec);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}
