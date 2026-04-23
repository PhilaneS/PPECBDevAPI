using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public required string CategoryCode { get; set; }
        public List<Product>? Products { get; set; }
        public required bool IsActive { get; set; }
       
    }
}
