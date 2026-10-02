using IncidentManagement.Domain.Incidents;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.Infrastructure.Persistence;

public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Incident> Incidents => Set<Incident>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var incident = modelBuilder.Entity<Incident>();

        incident.HasKey(item => item.Id);
        incident.Property(item => item.Title).HasMaxLength(200).IsRequired();
        incident.Property(item => item.Description).HasMaxLength(4000).IsRequired();
        incident.Property(item => item.Priority).HasConversion<string>().IsRequired();
        incident.Property(item => item.Status).HasConversion<string>().IsRequired();
    }
}
