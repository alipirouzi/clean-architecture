using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseEntityConfigurations;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
        
        builder.Property(x => x.Name);
        builder.Property(x => x.OrderNumber);
        builder.Property(x => x.CreatedAtUtc);
        
        builder.HasIndex(x => x.CreatedAtUtc);
        builder.HasIndex(x => x.OrderNumber);
    }
}