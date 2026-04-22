namespace Application.DTOs
{
    public class CategoryResponseDto
    {
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public required string CategoryCode { get; set; }
        public bool IsActive { get; set; }
    }
}
