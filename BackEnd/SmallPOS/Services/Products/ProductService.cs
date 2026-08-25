using SmallPOS.API.DTOs.Products;
using SmallPOS.API.Models;
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

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(MapToDto);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return null;

        return MapToDto(product);
    }

    public async Task<int> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            SKU = dto.SKU,
            CategoryId = dto.CategoryId,
            PurchasePrice = dto.PurchasePrice,
            SalePrice = dto.SalePrice,
            Stock = dto.Stock,
            Status = dto.Status
        };

        return await _productRepository.CreateAsync(product);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = new Product
        {
            Id = id,
            Name = dto.Name,
            SKU = dto.SKU,
            CategoryId = dto.CategoryId,
            PurchasePrice = dto.PurchasePrice,
            SalePrice = dto.SalePrice,
            Stock = dto.Stock,
            Status = dto.Status
        };

        return await _productRepository.UpdateAsync(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            SKU = product.SKU,
            CategoryId = product.CategoryId,
            PurchasePrice = product.PurchasePrice,
            SalePrice = product.SalePrice,
            Stock = product.Stock,
            Status = product.Status
        };
    }
}