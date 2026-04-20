using API.Response;
using Application.Common.Models;
using Application.Common.Requests;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetProducts([FromQuery] PagedRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);           
            var products = await _productService.GetPagedAsync(userId,request);
            return Ok(ApiResponse<PagedResponse<ProductDto>>.SuccessResponse(products));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id) 
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var products = await _productService.GetProductByIdAsync(id);
            return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(products));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto productDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var createdProduct = await _productService.CreateAsync(productDto, userId);
            return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(createdProduct));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto productDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
           var updatedProduct = await _productService.UpdateAsync(productDto, userId);
            if (updatedProduct.Success)
                return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(updatedProduct.Data));

            return StatusCode(StatusCodes.Status409Conflict, ApiResponse<ProductResponseDto>.Failure("Concurrency conflict occurred", updatedProduct.Data));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _productService.DeleteAProductAsync(id, userId);
            return Ok(ApiResponse<string>.SuccessResponse("Product deleted successfully."));
        }

        [HttpPost("upload-excel")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadExcel([FromForm] UploadExcelRequest request) 
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (request.File == null || request.File.Length == 0)
                return BadRequest("File is required");

            await _productService.ImportProductsAsync(request.File, userId);
            return Ok(ApiResponse<string>.SuccessResponse("Excel file uploaded and processed successfully."));
        }
        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportExcel()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var fileContent = await _productService.ExportProductsAsync(userId);
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "products.xlsx");
        }
    }
}
