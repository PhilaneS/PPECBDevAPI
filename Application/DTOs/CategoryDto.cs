namespace Application.DTOs
{
    public class CategoryDto
    {
        public required string Name { get; set; }
        public required string CategoryCode { get; set; }
        public bool IsActive { get; set; }
    }
}
