using Microsoft.EntityFrameworkCore;

namespace OrmBenchmark;

public class MyDbContext : DbContext
{
    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<Sale> sales { get; set; }
    public DbSet<Customer> customers { get; set; }
    public static string connectionString = "Server=localhost; User ID=root; Password=root; Database=shopdb;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table mappings
        modelBuilder.Entity<Category>().ToTable("CATET01").HasKey(c => c.T01F01);
        modelBuilder.Entity<Product>().ToTable("PRODT02").HasKey(p => p.T02F01);
        modelBuilder.Entity<Customer>().ToTable("CUSTT03").HasKey(c => c.T03F01);
        modelBuilder.Entity<Sale>().ToTable("SALEST04").HasKey(s => s.T04F01);

        // Relationships
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.T02F02);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Product)
            .WithMany(p => p.Sales)
            .HasForeignKey(s => s.T04F02);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Customer)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.T04F03);
    }
}
