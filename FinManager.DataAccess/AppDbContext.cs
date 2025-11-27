using FinManager.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace FinManager.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<Currency> Currencies { get; init; }
    public DbSet<Transaction> Transactions { get; init; }
    public DbSet<User> Users { get; init; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
