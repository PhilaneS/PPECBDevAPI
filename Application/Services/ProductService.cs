using Application.Common.Models;
using Application.Common.Requests;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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

        public async Task<IEnumerable<ProductDto>> GetByUserIdAsync(int userId)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetByUserIdAsync(userId));
        }

        public async Task CreateAsync(CreateProductDto productDto, int userId)
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
            await _productRepository.CreateAsync(product);

        }

        public async Task UpdateAsync(UpdateProductDto productDto, int userId)
        {

            var product = _mapper.Map<Product>(productDto);

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
            product.UpdatedBy = userId;
            product.UpdatedDate = DateTime.UtcNow;



            product.UserId = userId;
            await _productRepository.UpdateAsync(product);

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
                await _productRepository.CreateAsync(mappedProduct);
            }
            return products;
        }

        public async Task<byte[]> ExportProductsAsync(int userId)
        {
            var products = await _productRepository.GetByUserIdAsync(userId);
            return _excelService.ExportProductsToExcelAsync(_mapper.Map<List<ProductDto>>(products));
        }

        public async Task<PagedResponse<ProductDto>> GetPagedAsync(int userId, PagedRequest request)
        {
            var (products, totalCount) = await _productRepository.GetPagedAsync(userId, request.PageNumber, request.PageSize);

            var mapped = _mapper.Map<List<ProductDto>>(products);

            return new PagedResponse<ProductDto> 
            { 
                Data = mapped, 
                PageNumber = request.PageNumber, 
                PageSize = request.PageSize, 
                TotalRecords = totalCount 
            };
        }
    }
}
