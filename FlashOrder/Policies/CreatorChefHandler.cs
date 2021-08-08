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
            
            var Id = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            if (context.User.HasClaim(ClaimTypes.NameIdentifier, resource.ChefId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}