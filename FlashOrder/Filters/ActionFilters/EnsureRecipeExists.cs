using System;
using System.Linq;
using FlashOrder.Data;
using FlashOrder.IRepository;
using FlashOrder.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlashOrder.Filters.ActionFilters
{
    public class EnsureRecipeExists : IActionFilter
    {
        private readonly DatabaseContext _context;

        public EnsureRecipeExists(DatabaseContext context)
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

            var recipe = _context.Recipes.SingleOrDefault(x=>x.Id==id);
            // var recipe = _unitOfWork.Recipes.Get(r => r.Id==id);
            
            if (recipe == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("recipe", recipe);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}