using Application.Interfaces;
using Application.Services;
using AutoMapper;
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
    }
}
