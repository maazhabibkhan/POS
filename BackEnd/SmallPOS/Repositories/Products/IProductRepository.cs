using SmallPOS.API.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallPOS.API.Repositories.Products;

public interface IProductRepository
{
    Task<IEnumerable<ProductResponse>> GetAllAsync();

    Task<ProductResponse?> GetByIdAsync(int id);

    Task<int> CreateAsync(ProductRequest request);

    Task<bool> UpdateAsync(int id, ProductRequest request);

    Task<bool> DeleteAsync(int id);
}
