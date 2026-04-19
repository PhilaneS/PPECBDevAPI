namespace Application.DTOs
{
    public class UpdateCategoryDto
    {
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public required string CategoryCode { get; set; }
        public required bool IsActive { get; set; }
    }
}
