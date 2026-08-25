using SmallPOS.API.DTOs.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallPOS.API.Services.Products;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateProductDto dto);

    Task<bool> UpdateAsync(int id, UpdateProductDto dto);

    Task<bool> DeleteAsync(int id);
}