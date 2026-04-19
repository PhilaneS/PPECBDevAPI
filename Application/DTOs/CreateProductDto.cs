using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int CategoryId { get; set; }

        public string? Description { get; set; }
        public required string CategoryName { get; set; }
        public IFormFile? Image { get; set; }

        public string? ImagePublicId { get; set; }

    }
}
