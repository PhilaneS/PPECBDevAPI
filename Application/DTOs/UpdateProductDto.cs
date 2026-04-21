using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ProductCode { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public  byte[]? RowVersion { get; set; }

    }
}
