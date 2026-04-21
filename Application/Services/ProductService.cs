using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Common.Requests;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCodeGenerator _productCodeGenerator;
        private readonly IImageService _imageService;
        private readonly IExcelService _excelService;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository, 
            IProductCodeGenerator productCodeGenerator,
            IImageService imageService,
            IExcelService excelService, 
            IMapper mapper )
        {
            _productRepository = productRepository;
            _productCodeGenerator = productCodeGenerator;
            _imageService = imageService;
            _excelService = excelService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetByUserIdAsync(int userId)
        {
            return _mapper.Map<IEnumerable<ProductResponseDto>>(await _productRepository.GetByUserIdAsync(userId));
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id) 
        {
            return _mapper.Map<ProductResponseDto>(await _productRepository.GetByIdAsync(id));
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto productDto, int userId)
        {
            var productCode = await _productCodeGenerator.GenerateProductCodeAsync();
            var product = _mapper.Map<Product>(productDto);

            if (productDto.Image != null)
            {
                var (imageUrl, publicId) = await _imageService.UploadImageAsync(productDto.Image, userId);
                product.ImageUrl = imageUrl;
                product.ImagePublicId = publicId;
            }
            product.ProductCode = productCode;
            product.UserId = userId;
            product.CreatedBy = userId;
            product.CreatedDate = DateTime.UtcNow;

          var createdProduct = await _productRepository.CreateAsync(product);

            return _mapper.Map<ProductResponseDto>(createdProduct);
        }

        public async Task<ResultResponse<ProductResponseDto>> UpdateAsync(UpdateProductDto productDto, int userId)
        {
            var product = await _productRepository.GetByIdAsync(productDto.Id);

            if (productDto.Image != null)
            {

                var (imageUrl, publicId) = await _imageService.UploadImageAsync(productDto.Image, userId);

                if (!string.IsNullOrEmpty(product.ImagePublicId))
                {
                    await _imageService.DeleteAsync(product.ImagePublicId);
                }

                product.ImageUrl = imageUrl;
                product.ImagePublicId = publicId;
            }

            product.Name = productDto.Name ?? product.Name;
            product.Description = productDto.Description ?? product.Description;
            product.Price = productDto.Price != default ? productDto.Price : product.Price;
            product.CategoryId = productDto.CategoryId ?? product.CategoryId;
            product.ProductCode = productDto.ProductCode ?? product.ProductCode;
            product.CategoryName = productDto.CategoryName ?? product.CategoryName;
            product.UpdatedBy = userId;
            product.UpdatedDate = DateTime.UtcNow;
            product.UserId = userId;


            try
            {
              var updatedProduct = await _productRepository.UpdateAsync(product, productDto.RowVersion);

               var mappedProduct = _mapper.Map<ProductResponseDto>(updatedProduct);               

                return new ResultResponse<ProductResponseDto> { Success = true, Data = mappedProduct };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.First();
               
                var databaseEntry = entry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    throw new NotFoundException("The product was deleted by another user.");
                }
       
                var databaseValues = (Product)databaseEntry.ToObject();

                var results = new ProductResponseDto
                {
                    Id = databaseValues.Id,
                    Name = databaseValues.Name,
                    Description = databaseValues.Description,
                    Price = databaseValues.Price,
                    CategoryId = databaseValues.CategoryId,
                    ProductCode = databaseValues.ProductCode,
                    CategoryName = databaseValues.CategoryName,
                    ImageUrl = databaseValues.ImageUrl,
                    RowVersion = databaseValues.RowVersion
                };

                return new ResultResponse<ProductResponseDto> 
                { 
                    Success = false, 
                    Data = results 
                };

            }
        }

        public async Task DeleteAProductAsync(int id, int userId)
        {
            await _productRepository.DeleteAsync(id,userId);
        }
        public async Task<List<ProductDto>> ImportProductsAsync(IFormFile file, int userId)
        {
            var products = await _excelService.ImportProductsAsync(file);

            foreach (var product in products)
            {
                var mappedProduct = _mapper.Map<Product>(product);
                var productCode = await _productCodeGenerator.GenerateProductCodeAsync();
                mappedProduct.ProductCode = productCode;
                mappedProduct.UserId = userId;
                mappedProduct.CreatedBy = userId;
                mappedProduct.CreatedDate = DateTime.UtcNow;

                await _productRepository.CreateAsync(mappedProduct);
            }
            return products;
        }

        public async Task<byte[]> ExportProductsAsync(int userId)
        {
            var products = await _productRepository.GetByUserIdAsync(userId);
            return _excelService.ExportProductsToExcelAsync(_mapper.Map<List<ProductDto>>(products));
        }

        public async Task<PagedResponse<ProductResponseDto>> GetPagedAsync(int userId, PagedRequest request)
        {
            var (products, totalCount) = await _productRepository.GetPagedAsync(userId, request.PageNumber, request.PageSize);

            var mapped = _mapper.Map<List<ProductResponseDto>>(products);

            return new PagedResponse<ProductResponseDto> 
            { 
                Data = mapped, 
                PageNumber = request.PageNumber, 
                PageSize = request.PageSize, 
                TotalRecords = totalCount 
            };
        }
    }
}
