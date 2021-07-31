using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FlashOrder.Data;
using FlashOrder.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FlashOrder.Repository
{
    public class RecipeRepository:IRecipeRepository
    {
        
        private readonly DatabaseContext _context;
        private readonly DbSet<Recipe> _db;

        public RecipeRepository(DatabaseContext context)
        {
            _context = context;
            _db = _context.Set<Recipe>();
        }
        

        public async Task<IList> GetAll(Expression<Func<Recipe, bool>> expression = null,
            Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>> orderBy = null, List<string> includes = null)
        {
            IQueryable<Recipe> query=_db;
            
            if (expression!=null)
            {
                query=query.Where(expression);
            }
            
            if (includes!=null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy!=null)
            {
                query = orderBy(query);
            }

            //here we ask him for not tracking the object status
            return await query.AsNoTracking().ToListAsync();
        }
        

        //The expression can be a lambda expression 
        public async Task<Recipe> Get(Expression<Func<Recipe, bool>> expression, List<string> includes=null)
        {
            IQueryable<Recipe> query=_db;
            if (includes!=null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task Insert(Recipe entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<Recipe> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Recipe> entities)
        {
            _db.RemoveRange(entities);
        }

        public void Update(Recipe entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IList> GetAllWithFilters(Expression<Func<Recipe, bool>> expression = null, Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>> orderBy = null, List<string> includes = null,
            RecipeParameters filters = null)
        {
            
            IQueryable<Recipe> query=_db;
            
            if (expression!=null)
            {
                query=query.Where(expression);
            }
            
            if (includes!=null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy!=null)
            {
                query = orderBy(query);
            }

            if (filters!=null)
            {
                if (filters.Ingredients!=null)
                {
                    query=query.Where(r => r.Ingredients
                        .Any(ing => filters.Ingredients.Any(queredIngredient=>ing.Item.Name.Equals(queredIngredient))));
                }

                if (!string.IsNullOrEmpty(filters.Title))
                {
                    query = query.Where(r => r.Title.Contains(filters.Title));
                }
            }

            //here we ask him for not tracking the object status
            return await query.AsNoTracking().ToListAsync();
        }
    }
}