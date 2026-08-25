using Microsoft.AspNetCore.Mvc;

using SmallPOS.API.DTOs.Products;
using SmallPOS.API.Services.Products;
using System.Threading.Tasks;

namespace SmallPOS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    // GET: api/Product

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }


    // GET: api/Product/5

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(product);
    }


    // POST: api/Product

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductDto dto)
    {
        var id = await _productService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new
            {
                id,
                message = "Product created successfully."
            }
        );
    }


    // PUT: api/Product/5

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductDto dto)
    {
        var updated = await _productService.UpdateAsync(
            id,
            dto
        );

        if (!updated)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(new
        {
            message = "Product updated successfully."
        });
    }


    // DELETE: api/Product/5

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(new
        {
            message = "Product deleted successfully."
        });
    }
}