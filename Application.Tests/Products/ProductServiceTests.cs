using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Products
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IProductCodeGenerator> _productCodeGeneratorMock;
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly Mock<IExcelService> _excelServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _productService;
        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productCodeGeneratorMock = new Mock<IProductCodeGenerator>();
            _imageServiceMock = new Mock<IImageService>();
            _excelServiceMock = new Mock<IExcelService>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(
                _productRepositoryMock.Object,
                _productCodeGeneratorMock.Object,
                _imageServiceMock.Object,
                _excelServiceMock.Object,
                _mapperMock.Object
            );
        }

        //[Fact]
        //public async Task CreateAsync_WithoutImage_AssignsProductCodeAndPersists()
        //{
        //    // Arrange
        //    var userId = 42;
        //    var createDto = new CreateProductDto
        //    {
        //        Name = "Product 1",
        //        Price = 9.99m,
        //        CategoryId = 1,
        //        CategoryName = "Categorty 1"
        //    };

        //    var generatedCode = "CODE-001";
        //    _productCodeGeneratorMock.Setup(g => g.GenerateProductCodeAsync())
        //        .ReturnsAsync(generatedCode);

        //   Product captured = null!;
        //    _productRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Product>()))
        //        .ReturnsAsync((Product p) =>
        //        {
        //            p.Id = 100;
        //            captured = p;
        //            return p;
        //        });

            

        //    // Act
        //    var result = await _productService.CreateAsync(createDto, userId);

        //    // Assert
        //    _productRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Product>()), Times.Once);
        //    Assert.NotNull(captured);
        //    Assert.Equal(generatedCode, captured.ProductCode);
        //    Assert.Equal(userId, captured.UserId);
        //    Assert.Equal(userId, captured.CreatedBy);
        //    Assert.True(captured.CreatedDate <= DateTime.UtcNow && captured.CreatedDate > DateTime.UtcNow.AddMinutes(-1));
        //    Assert.Equal(generatedCode, result.ProductCode);
        //}

        //[Fact]
        //public async Task CreateAsync_WithImage_UploadsImageAndSetsUrlAndPublicId()
        //{
        //    // Arrange
        //    var userId = 7;
        //    var createDto = new CreateProductDto
        //    {
        //        Name = "Product 2 image",
        //        Price = 1.23m,
        //        CategoryId = 2,
        //        CategoryName = "Category 2",
        //        Image = Mock.Of<IFormFile>()
        //    };

        //    var generatedCode = "PCODE-IMG";
        //    _productCodeGeneratorMock.Setup(g => g.GenerateProductCodeAsync())
        //        .ReturnsAsync(generatedCode);

        //    var returnedUrl = "https://cdn/image.jpg";
        //    var returnedPublicId = "public-123";
        //    _imageServiceMock.Setup(s => s.UploadImageAsync(createDto.Image!, userId))
        //        .ReturnsAsync((returnedUrl, returnedPublicId));

        //    Product captured = null!;
        //    _productRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Product>()))
        //        .ReturnsAsync((Product p) =>
        //        {
                   
        //            p.Id = 55;
        //            captured = p;
        //            return p;
        //        });

        //    // Act
        //    var result = await _productService.CreateAsync(createDto, userId);

        //    // Assert
        //    _imageServiceMock.Verify(s => s.UploadImageAsync(createDto.Image!, userId), Times.Once);
        //    _productRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Product>()), Times.Once);
        //    Assert.NotNull(captured);
        //    Assert.Equal(returnedUrl, captured.ImageUrl);
        //    Assert.Equal(returnedPublicId, captured.ImagePublicId);
        //    Assert.Equal(generatedCode, captured.ProductCode);
        //    Assert.Equal(generatedCode, result.ProductCode);
        //}
    }
}
