using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Outbox;

namespace Persistence;

public class ApplicationDbContext(DbContextOptions options) :
    DbContext(options), IApplicationDbContext, IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyInfo.Assembly);
    
    public DbSet<Order> Orders { get; set; }
}