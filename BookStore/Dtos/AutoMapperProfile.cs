using AutoMapper;
using BookStore.Dtos.CategoryDtos;
using Domain.Entities;

namespace BookStore.Dtos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
