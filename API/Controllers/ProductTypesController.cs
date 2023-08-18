

using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/{controller}")]
public class ProductTypesController : ControllerBase
{
    private readonly IProductTypeRepository _repo;

    public ProductTypesController(IProductTypeRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
        var productTypes = await _repo.GetProductTypesAsync();

        if(productTypes == null)
        {
            return NotFound();
        }

        return Ok(productTypes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductType>> GetProductTypeById(int id)
    {
        var productType = await _repo.GetProductTypeByIdAsync(id);

        if(productType == null)
        {
            return NotFound();
        }

        return Ok(productType);
    }
}