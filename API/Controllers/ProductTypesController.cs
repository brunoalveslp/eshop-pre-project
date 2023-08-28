

using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ProductTypesController : BaseApiController
{
    private readonly IGenericRepository<ProductType> _repo;

    public ProductTypesController(IGenericRepository<ProductType> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
        var productTypes = await _repo.GetAllAsync();

        if(productTypes == null)
        {
            return NotFound(new ApiResponse(404));
        }

        return Ok(productTypes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductType>> GetProductTypeById(int id)
    {
        var productType = await _repo.GetByIdAsync(id);

        if(productType == null)
        {
            return NotFound(new ApiResponse(404));
        }

        return Ok(productType);
    }
}