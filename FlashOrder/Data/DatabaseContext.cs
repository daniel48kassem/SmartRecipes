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

            // builder.Entity<ApiUser>()
            //     .HasMany(u => u.)
            //     .WithMany(u => u.FriendOf);
            //     .Map(m => m.ToTable("UserFriends")
            //     .MapLeftKey("UserId")
            //     .MapRightKey("FriendId"));
            
            builder.ApplyConfiguration(new ItemConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Step> Steps { get; set; }

        public DbSet<Follow> Follows { get; set; }
    }
}