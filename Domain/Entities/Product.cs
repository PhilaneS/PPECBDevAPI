using Domain.Common;
using Domain.Entities;

namespace Domain.Entities
{
    public class Product: BaseEntity
    {
        public int Id { get; set; }
        public required string ProductCode { get; set; }       
        public required string Name { get; set; }
        public string? Description { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public required string CategoryName { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImagePublicId { get; set; }
        public required byte[] RowVersion { get; set; }

    }
}
