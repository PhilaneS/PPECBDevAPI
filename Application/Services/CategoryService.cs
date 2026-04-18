using Application.Common.Exceptions;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetByUserIdAsync(int userId)
        {
            var categories = await _categoryRepository.GetByUserIdAsync(userId);
            if (categories == null)
                return Enumerable.Empty<CategoryDto>();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<Result> CreateAsync(CategoryDto categoryDto, int userId)
        {
            if (!Regex.IsMatch(categoryDto.CategoryCode, Constants.CategoryCodePattern))
                throw new ValidationException("Invalid format. Use ABC123.");

            var exists = await _categoryRepository.CodeExistsAsync(categoryDto.CategoryCode);
            if (exists)
                throw new ValidationException("Category code already exists.");

            var category = new Category
            {
                Name = categoryDto.Name,
                CategoryCode = categoryDto.CategoryCode,
                IsActive = categoryDto.IsActive,
                UserId = userId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId
            };

            await _categoryRepository.CreateAsync(category);

            return Result.Ok();
        }
        public async Task<CategoryDto> GetByIdAsync(int id, int userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id, userId);
            if (category == null)
            {
                throw new NotFoundException("Category not Found");
            }
            return _mapper.Map<CategoryDto>(category);
        }       

    }
}
