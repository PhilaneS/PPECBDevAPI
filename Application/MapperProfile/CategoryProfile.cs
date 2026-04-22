using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MapperProfile
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile() 
        {
            // CategoryDto <-> Category
            CreateMap<CategoryDto, Category>().ReverseMap();

            // Category <-> UpdateCategoryDto
            CreateMap<Category, UpdateCategoryDto>()
              .ReverseMap();

            // Category <-> CategoryResponseDto
            CreateMap<Category, CategoryResponseDto>()
                //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            //Category <-> CategoryOptionResponseDto
            CreateMap<Category, CategoryOptionResponseDto>()
                //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

        }
    }
}
