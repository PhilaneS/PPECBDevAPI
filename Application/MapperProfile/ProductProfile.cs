using AutoMapper;
using Application.DTOs;
using Domain.Entities;

namespace Application.MapperProfile
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            // CreateProductDto -> Product
            CreateMap<CreateProductDto, Product>() 
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<Product, ProductResponseDto>();

            // Product -> CreateProductDto
            CreateMap<Product, UpdateProductDto>()
                .ForSourceMember(src => src.User, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Category, opt => opt.DoNotValidate()).ReverseMap();

            // UpdateProductDto -> Product
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            // Product -> CreateProductDto
            CreateMap<Product, CreateProductDto>()
                .ForSourceMember(src => src.User, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Category, opt => opt.DoNotValidate()).ReverseMap();



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

        }
    }
}
