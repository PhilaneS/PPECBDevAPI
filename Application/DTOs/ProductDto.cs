using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class ProductDto
    {       
        public  string? Name { get; set; }
        public  string? ProductCode { get; set; }
        public  int? CategoryId { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
