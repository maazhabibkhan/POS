using SmallPOS.API.Models.Products;
using SmallPOS.API.Repositories.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallPOS.API.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        return _productRepository.GetAllAsync();
    }

    public Task<ProductResponse?> GetByIdAsync(int id)
    {
        return _productRepository.GetByIdAsync(id);
    }

    public async Task<ProductResponse?> CreateAsync(ProductRequest request)
    {
        var id = await _productRepository.CreateAsync(request);

        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<ProductResponse?> UpdateAsync(
        int id,
        ProductRequest request)
    {
        var updated = await _productRepository.UpdateAsync(id, request);

        if (!updated)
        {
            return null;
        }

        return await _productRepository.GetByIdAsync(id);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _productRepository.DeleteAsync(id);
    }
}
