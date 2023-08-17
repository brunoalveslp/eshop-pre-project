using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    // we sufix it with async becouse it's easy to know we need to await, and we can await
    Task<Product> GetProductByIdAsync(int id);
    // IReadOnlyList needs to know it's own type, and we can only read so it's lighter than a simple list
    Task<IReadOnlyList<Product>> GetProductsAsync();
}