
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Detector.Contract.Models;

namespace Detector.WebApi.Data;

public class DetectorContext : IdentityDbContext<DetectorUser>
{
    public DetectorContext(DbContextOptions<DetectorContext> options) : base(options) 
    {
    }

    public DbSet<DetectorData> DetectorData { get; set; } = null;

    public DbSet<DetectorDetails> DetectorDetails { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
