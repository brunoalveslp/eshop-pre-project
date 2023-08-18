
using Core.Entities;

namespace Core.Interfaces;

public interface IProductBrandRepository
{
    Task<ProductBrand> GetProductBrandByIdAsync(int id);

    Task<IReadOnlyList<ProductBrand>> GetProductBrands();
}