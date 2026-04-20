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
        public string? ImageUrl { get; set; } = null;
        public IFormFile? Image { get; set; }
        public string? ImagePublicId { get; set; }
        public required byte[] RowVersion { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
