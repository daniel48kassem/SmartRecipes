using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;

namespace FlashOrder.Configurations
{
    public class MapperInitializer:Profile
    {
        public MapperInitializer()
        {
            CreateMap<Recipe,RecipeDTO>().ReverseMap();
        }
    }
}