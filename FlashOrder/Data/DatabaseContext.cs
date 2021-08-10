using FlashOrder.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// using Microsoft.AspNetCore.Identity.;
namespace FlashOrder.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //define many to many relation between the user and chef as follow relation
            builder.Entity<Follow>()
                .HasKey(bc => new {bc.ChefId, bc.FollowerId});

            builder.Entity<Follow>()
                .HasOne(bc => bc.Chef)
                .WithMany(b => b.ChefFollowers)
                .HasForeignKey(bc => bc.ChefId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(true);

            builder.Entity<Follow>()
                .HasOne(bc => bc.Follower)
                .WithMany(c => c.FollowedChefs)
                .HasForeignKey(bc => bc.FollowerId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(true);

            //many to many relation between user and recipe as rating
            builder.Entity<Rating>()
                .HasKey(bc => new {bc.RecipeId, bc.UserId});

            builder.Entity<Rating>()
                .HasOne(bc => bc.Recipe)
                .WithMany(b => b.Raters)
                .HasForeignKey(bc => bc.RecipeId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(true);

            builder.Entity<Rating>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.MyRatedRecipes)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(true);
            
            builder.ApplyConfiguration(new ItemConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Step> Steps { get; set; }

        public DbSet<Follow> Follows { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}