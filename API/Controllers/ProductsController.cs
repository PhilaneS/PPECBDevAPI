using Application.Common.Models;
using Application.Common.Requests;
using Application.DTOs;
using Application.Services;
using Azure.Core;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] PagedRequest request)
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            //var products = await _productService.GetByUserIdAsync(userId);
            var products = await _productService.GetPagedAsync(userId,request);
            return Ok(ApiResponse<PagedResponse<ProductDto>>.SuccessResponse(products));
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto productDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _productService.CreateAsync(productDto, userId);
            return Ok(ApiResponse<string>.SuccessResponse("Product created successfully."));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto productDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _productService.UpdateAsync(productDto, userId);
            return Ok(ApiResponse<string>.SuccessResponse("Product updated successfully."));
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
