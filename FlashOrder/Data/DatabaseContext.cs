using FlashOrder.Configurations.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlashOrder.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ItemConfiguration());
        }
        
        
        public DbSet<Image>Images { get; set; }
        public DbSet<Item>Items { get; set; }
        public DbSet<Ingredient>Ingredients { get; set; }
        public DbSet<Recipe>Recipes { get; set; }
        
    }
}