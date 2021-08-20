using System;
using System.Linq;
using FlashOrder.Data;
using FlashOrder.IRepository;
using FlashOrder.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlashOrder.Filters.ActionFilters
{
    public class EnsureChefExists : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var _userManager = (UserManager<ApiUser>) context.HttpContext
                .RequestServices.GetService(typeof(UserManager<ApiUser>));
                
            Guid id = Guid.Empty;
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (Guid) context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad chef id parameter");
                return;
            }

            var chef =  _userManager.FindByIdAsync(id.ToString());
            
            if (chef == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("chef", chef);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}