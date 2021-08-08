using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlashOrder.Data;

namespace FlashOrder.IRepository
{
    public interface IRecipeRepository:IGenericRepository<Recipe>
    {
          Task<IList> GetAllWithFilters(
            Expression<Func<Recipe,bool>> expression=null,
            Func<IQueryable<Recipe>,IOrderedQueryable<Recipe>> orderBy=null,
            List<string> includes=null,RecipeParameters filters=null
        );
        
          
    }
}