using Microsoft.EntityFrameworkCore;
/*
Get-Migrations
add-migration Initial_migration
remove-migration 
update-database
*/
namespace ShopApi.Entities
{
    public class ShopDBContext : DbContext
    {
        public ShopDBContext(DbContextOptions<ShopDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Shop> Shops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
               .Property(u => u.Name)
               .HasMaxLength(32);

            modelBuilder.Entity<User>()
               .Property(u => u.Surname)
               .HasMaxLength(150);

            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .HasMaxLength(150)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Phone)
               .HasMaxLength(20);

            modelBuilder.Entity<User>()
              .Property(u => u.IsActive)
              .HasDefaultValue(false);

            modelBuilder.Entity<User>()
              .Property(u => u.PasswordHash)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(u => u.CreatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAdd()
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(u => u.UpdatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAddOrUpdate()
              .IsRequired();

            modelBuilder.Entity<Role>()
              .Property(r => r.Name)
              .HasMaxLength(100)
              .IsRequired();

            modelBuilder.Entity<Role>()
              .Property(r => r.CreatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAdd()
              .IsRequired();

            modelBuilder.Entity<Role>()
              .Property(r => r.UpdatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAddOrUpdate()
              .IsRequired();

            modelBuilder.Entity<Product>()
              .Property(p => p.Name)
              .HasMaxLength(100)
              .IsRequired();

            modelBuilder.Entity<Product>()
              .Property(p => p.ShopId)
              .IsRequired();

            modelBuilder.Entity<Product>()
              .Property(p => p.Price)
              .HasColumnType("decimal(10,4)")
              .IsRequired();

            modelBuilder.Entity<Product>()
            .Property(p => p.Category)
            .HasMaxLength(200);

            modelBuilder.Entity<Product>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAdd()
              .IsRequired();

            modelBuilder.Entity<Product>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAddOrUpdate()
              .IsRequired();

           modelBuilder.Entity<Address>()
              .Property(a => a.City)
              .HasMaxLength(50);

           modelBuilder.Entity<Address>()
              .Property(a => a.Street)
              .HasMaxLength(100);
          
           modelBuilder.Entity<Address>()
               .Property(a => a.PostalCode)
               .HasMaxLength(10);


            modelBuilder.Entity<Address>()
              .Property(a => a.CreatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAdd()
              .IsRequired();

            modelBuilder.Entity<Address>()
              .Property(a => a.UpdatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAddOrUpdate()
              .IsRequired();



            modelBuilder.Entity<Shop>()
              .Property(s => s.Name)
              .HasMaxLength(100)
              .IsRequired();
           
            modelBuilder.Entity<Shop>()
              .Property(s => s.HasDelivery)
              .HasDefaultValue(false);
           
            modelBuilder.Entity<Shop>()
                .Property(s => s.ContactEmail)
                .HasMaxLength(150);

            modelBuilder.Entity<Shop>()
                .Property(s => s.Website)
                .HasMaxLength(200);

            modelBuilder.Entity<Shop>()
                .Property(s => s.Category)
                .HasMaxLength(200);

            modelBuilder.Entity<Shop>()
                .Property(s => s.Phone)
                .HasMaxLength(9);

            modelBuilder.Entity<Shop>()
              .Property(s => s.CreatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAdd()
              .IsRequired();

            modelBuilder.Entity<Shop>()
              .Property(s => s.UpdatedAt)
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAddOrUpdate()
              .IsRequired();

        }
    }
}
