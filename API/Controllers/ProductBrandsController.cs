

using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/{controller}")]

public class ProductBrandsController : ControllerBase
{
    private readonly IGenericRepository<ProductBrand> _repo;
    public ProductBrandsController(IGenericRepository<ProductBrand> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
        var productBrands = await _repo.GetAllAsync();

        if(productBrands == null)
        {
            return NotFound();
        }

        return Ok(productBrands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
    {
        var productBrand = await _repo.GetByIdAsync(id);

        if(productBrand == null)
        {
            return NotFound();
        }

        return Ok(productBrand);
    }
}