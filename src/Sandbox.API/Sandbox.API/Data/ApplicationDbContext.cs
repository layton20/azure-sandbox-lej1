using Microsoft.EntityFrameworkCore;
using Sandbox.API.Entities;

namespace Sandbox.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<CustomerEntity> Customers { get; set; }
}