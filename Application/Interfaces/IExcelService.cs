using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IExcelService
    {
        byte[] ExportProductsToExcelAsync(List<ProductDto> products);
        Task<List<ProductDto>> ImportProductsAsync(IFormFile file);

    }
}
