using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FlashOrder.Data;
using FlashOrder.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace FlashOrder.Policies
{
    public class OnlyChefOfRecipe: AuthorizationHandler<OnlyChefOfRecipeRequirement,UpdateRecipeDTO>
    {
        private readonly IRecipeIdProvider _recipeIdProvider;

        public OnlyChefOfRecipe(IRecipeIdProvider recipeIdProvider)
        {
            _recipeIdProvider = recipeIdProvider;
        }
        
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyChefOfRecipeRequirement requirement,UpdateRecipeDTO recipe)
        {
            Console.WriteLine("qqq");
            // Console.WriteLine(context.User.Claims.);
            // if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            // {
            //     context.Fail();
            //     return Task.CompletedTask;
            // }
            
            var Id = context.User.FindFirst(c => c.Type == ClaimTypes.PrimarySid).Value;

            Console.WriteLine("dd Id");
            Console.WriteLine(Id);
            
            // if (recipe.ChefId == Id)
            // {
            //     context.Succeed(requirement);
            // }
            
            
            context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}