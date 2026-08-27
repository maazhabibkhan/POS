using SmallPOS.API.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallPOS.API.Services.Products;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllAsync();

    Task<ProductResponse?> GetByIdAsync(int id);

    Task<ProductResponse?> CreateAsync(ProductRequest request);

    Task<ProductResponse?> UpdateAsync(int id, ProductRequest request);

    Task<bool> DeleteAsync(int id);
}
