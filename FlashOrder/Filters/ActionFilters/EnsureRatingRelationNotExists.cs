using System;
using System.Linq;
using System.Security.Claims;
using FlashOrder.Data;
using FlashOrder.IRepository;
using FlashOrder.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlashOrder.Filters.ActionFilters
{
    public class EnsureRatingRelationNotExists : IActionFilter
    {
        private readonly DatabaseContext _context;

        public EnsureRatingRelationNotExists(DatabaseContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id = -1;
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (int) context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            var userId = context.HttpContext.User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)
                .Value;
            
            var recipeId = id;
        
            var rating = _context.Ratings.SingleOrDefault(x=>x.RecipeId==recipeId && x.UserId==userId);
            
            if (rating!=null)
            {
                context.Result = new BadRequestObjectResult("You Already Have Rating for this recipe");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}