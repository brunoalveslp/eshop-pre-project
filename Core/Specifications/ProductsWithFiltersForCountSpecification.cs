﻿using Core.Entities;
using Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications;

public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
       : base(x =>
               (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
               (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId) &&
               (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
       )
    { }

}
