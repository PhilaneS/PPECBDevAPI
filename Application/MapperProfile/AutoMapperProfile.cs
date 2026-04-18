using AutoMapper;
using Application.DTOs;
using Domain.Entities;

namespace Application.MapperProfile
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            // Product -> CreateProductDto
            CreateMap<Product, CreateProductDto>()
                //.ForMember(dest => dest.Image, opt => opt.Ignore())
                // Avoid mapping navigation objects into DTO
                .ForSourceMember(src => src.User, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Category, opt => opt.DoNotValidate()).ReverseMap();

            // CreateProductDto -> Product
            CreateMap<CreateProductDto, Product>()
                //.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                //.ForMember(dest => dest.Image, opt => opt.Ignore())
                // DTO does not carry navigation objects; preserve control in repository/service layer
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                // Id is an entity key managed by the DB; ignore when mapping from DTO unless explicitly set elsewhere
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            // Product -> ProductDto
            CreateMap<Product, ProductDto>()
                //.ForMember(dest => dest.Image, opt => opt.Ignore())
                // Avoid mapping navigation objects into DTO
                .ForSourceMember(src => src.User, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Category, opt => opt.DoNotValidate()).ReverseMap();

            // ProductDto -> Product
            CreateMap<ProductDto, Product>()
                  //.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                  //.ForMember(dest => dest.Image, opt => opt.Ignore())
                // DTO does not carry navigation objects; preserve control in repository/service layer
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                // Id is an entity key managed by the DB; ignore when mapping from DTO unless explicitly set elsewhere
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            // Category -> CategoryDto
            CreateMap<Category, CategoryDto>()
                // prevent mapping of product collection into simple DTO
                .ForSourceMember(src => src.Products, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.User, opt => opt.DoNotValidate()).ReverseMap();

            // CategoryDto -> Category
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()).ReverseMap();
        }
    }
}
