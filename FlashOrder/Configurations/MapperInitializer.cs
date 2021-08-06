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
            CreateMap<Recipe,CreateRecipeDTO>().ReverseMap();
            CreateMap<Recipe, RecipeDTO>().AfterMap((s,d)=>d.Cost=s.CalculateCost());

            CreateMap<Ingredient,IngredientDTO>().ReverseMap();
            CreateMap<Ingredient,CreateIngredientDTO>().ReverseMap();
            
            CreateMap<Item,ItemDTO>().ReverseMap();
            CreateMap<Item,CreateItemDTO>().ReverseMap();
            CreateMap<Item,GetItemDTO>().ReverseMap();
            
            CreateMap<Image,ImageDTO>().ReverseMap();
            CreateMap<Image,CreateImageDTO>().ReverseMap();
            
            
            CreateMap<ApiUser,UserDTO>().ReverseMap();
            CreateMap<ApiUser,RegisterDTO>().ReverseMap();
        }
    }
}