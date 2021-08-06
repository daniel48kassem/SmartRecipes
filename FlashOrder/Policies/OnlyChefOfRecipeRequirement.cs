using FlashOrder.Data;
using Microsoft.AspNetCore.Authorization;

namespace FlashOrder.Policies
{
    public class OnlyChefOfRecipeRequirement: IAuthorizationRequirement
    {
        public int Id { get; set; }

        public OnlyChefOfRecipeRequirement()
        {
        }
    }
}