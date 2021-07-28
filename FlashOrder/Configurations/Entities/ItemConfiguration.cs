using FlashOrder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashOrder.Configurations.Entities
{
    public class ItemConfiguration:IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasData(
                new Item
                {
                    Id = 1,
                    Name = "Egg",
                    Price = 300
                },
                new Item
                {
                    Id = 2,
                    Name = "Olive Oil",
                    Price = 8000
                }
            );
        }
    }
}