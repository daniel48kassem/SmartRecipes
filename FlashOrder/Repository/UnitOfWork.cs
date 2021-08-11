using System;
using System.Threading.Tasks;
using FlashOrder.Data;
using FlashOrder.IRepository;

namespace FlashOrder.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Item> _items;
        // private IGenericRepository<Recipe> _recipes;
        private IGenericRepository<Ingredient> _ingredients;
        private IGenericRepository<Step> _steps;
        private IGenericRepository<Follow> _follows;
        private IGenericRepository<Rating> _ratings;
        
        private IRecipeRepository _recipes;
        
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        // public IGenericRepository<Recipe> Recipes => _recipes ??= new GenericRepository<Recipe>(_context);
        public IGenericRepository<Item> Items=>_items ??= new GenericRepository<Item>(_context);
        public IGenericRepository<Step> Steps=>_steps ??= new GenericRepository<Step>(_context);
        public IRecipeRepository Recipes =>_recipes??=new RecipeRepository(_context);
        public IGenericRepository<Ingredient> Ingredients=> _ingredients??= new GenericRepository<Ingredient>(_context);
        public IGenericRepository<Follow> Follows=> _follows??= new GenericRepository<Follow>(_context);
        public IGenericRepository<Rating> Ratings=> _ratings??= new GenericRepository<Rating>(_context);
        
        public async Task save()
        {
            await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            //like garbage collector,like when i finish ,free the memory
            //free all the db context connections that the context was using  
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}