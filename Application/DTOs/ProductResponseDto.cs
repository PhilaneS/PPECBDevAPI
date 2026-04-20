using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public  string? Name { get; set; }
        public  string? Description { get; set; }
        public  string? ProductCode { get; set; }
        public   string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; } = null;
        public string? ImagePublicId { get; set; }
        public  byte[]? RowVersion { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
