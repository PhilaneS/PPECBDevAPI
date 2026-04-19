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

        public async Task<IEnumerable<CategoryResponseDto>> GetByUserIdAsync(int userId)
        {
            var categories = await _categoryRepository.GetByUserIdAsync(userId);
            if (categories == null || !categories.Any())
                throw new NotFoundException("Categories not found.");

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryResponseDto> CreateAsync(CategoryDto categoryDto, int userId)
        {
            if (!Regex.IsMatch(categoryDto.CategoryCode, Constants.CategoryCodePattern))
                throw new ValidationException("Invalid format. Use ABC123.");

            var exists = await _categoryRepository.CodeExistsAsync(categoryDto.CategoryCode, 0);
            if (exists)
                throw new ValidationException("Category code already exists.");

            var category = _mapper.Map<Category>(categoryDto);
            
            category.CreatedDate = DateTime.UtcNow;
            category.CreatedBy = userId;
            category.UserId = userId;
            var createdCategory =  await _categoryRepository.CreateAsync(category);

            return _mapper.Map<CategoryResponseDto>(createdCategory);
        }

        public async Task<CategoryResponseDto> UpdateAsync(UpdateCategoryDto updateCategoryDto, int userId)
        {


            if (!Regex.IsMatch(updateCategoryDto.CategoryCode, Constants.CategoryCodePattern))
                throw new ValidationException("Invalid format. Use ABC123.");

            var existingCategory = await _categoryRepository.GetByIdAsync(updateCategoryDto.CategoryId, userId);
            if (existingCategory == null)
                throw new NotFoundException($"Category with Id {updateCategoryDto.CategoryId} not found.");

            if (!string.Equals(existingCategory.CategoryCode, updateCategoryDto.CategoryCode, StringComparison.OrdinalIgnoreCase))
            {
                var exists = await _categoryRepository.CodeExistsAsync(updateCategoryDto.CategoryCode, updateCategoryDto.CategoryId);
                if (exists)
                {
                    throw new ValidationException("Category code already exists.");
                }
            }

            existingCategory.Name = updateCategoryDto.Name;
            existingCategory.IsActive = updateCategoryDto.IsActive;
            existingCategory.CategoryCode = updateCategoryDto.CategoryCode;
            existingCategory.UpdatedDate = DateTime.UtcNow;
            existingCategory.UpdatedBy = userId;
            existingCategory.UserId = userId;

            await _categoryRepository.UpdateAsync(existingCategory);

            return _mapper.Map<CategoryResponseDto>(existingCategory);
        }
        public async Task<CategoryResponseDto> GetByIdAsync(int id, int userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id, userId);
            if (category == null)
            {
                throw new NotFoundException("Category not Found");
            }
            return _mapper.Map<CategoryResponseDto>(category);
        }       

    }
}
