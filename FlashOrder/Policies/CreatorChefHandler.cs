using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FlashOrder.Data;
using Microsoft.AspNetCore.Authorization;

namespace FlashOrder.Policies
{
    public class CreatorChefHandler : AuthorizationHandler<CreatorChefRequirement, Recipe>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CreatorChefRequirement requirement,
            Recipe resource)
        {
            Console.WriteLine("DANIEL Resss");
            Console.WriteLine(resource.Description);
            Console.WriteLine(resource.ChefId);

            
            var Id = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Console.WriteLine(Id);
            
            if (context.User.HasClaim(ClaimTypes.NameIdentifier, resource.ChefId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}