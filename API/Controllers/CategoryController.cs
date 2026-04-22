using API.Response;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetCategories()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var categories = await _categoryService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<CategoryResponseDto>>.SuccessResponse(categories));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var createdCategory = await _categoryService.CreateAsync(categoryDto, userId);
            return Ok(ApiResponse<CategoryResponseDto>.SuccessResponse(createdCategory));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var category = await _categoryService.GetByIdAsync(id, userId);
            return Ok(ApiResponse<CategoryResponseDto>.SuccessResponse(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var updatedCategory = await _categoryService.UpdateAsync(updateCategoryDto, userId);
            return Ok(ApiResponse<string>.SuccessResponse("Category updated successfully."));
        }
    }
}
