using SmallPOS.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallPOS.API.Repositories.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);

    Task<int> CreateAsync(Product product);

    Task<bool> UpdateAsync(Product product);

    Task<bool> DeleteAsync(int id);
}