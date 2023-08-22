

using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification;

public class ProductsWithTypesAndBrandSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandSpecification()
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }

    public ProductsWithTypesAndBrandSpecification(int id) : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}