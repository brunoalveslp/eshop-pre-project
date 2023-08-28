

using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductBrandsController : BaseApiController
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
            return NotFound(new ApiResponse(404));
        }

        return Ok(productBrands);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
    {
        var productBrand = await _repo.GetByIdAsync(id);

        if(productBrand == null)
        {
            return NotFound(new ApiResponse(404));
        }

        return Ok(productBrand);
    }
}