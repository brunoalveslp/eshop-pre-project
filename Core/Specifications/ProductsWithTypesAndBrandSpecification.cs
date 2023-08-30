

using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification;

public class ProductsWithTypesAndBrandSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandSpecification(string sort, int? brandId, int? typeId)
        : base(x =>
                (!typeId.HasValue || x.ProductTypeId == typeId) &&
                (!brandId.HasValue || x.ProductBrandId == brandId) 
        )
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
        AddOrderBy(p => p.Name);

        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }

    public ProductsWithTypesAndBrandSpecification(int id) : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}