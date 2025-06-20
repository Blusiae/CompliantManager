using CompliantManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompliantManager.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses;
        public DbSet<Claim> Claims;
        //public DbSet<ClaimItem> ClaimItems;
        public DbSet<Customer> Customers;
        public DbSet<NotificationMethod> NotificationMethods;
        public DbSet<Order> Orders;
        public DbSet<OrderItem> OrderItems;
        public DbSet<Product> Products;
        public DbSet<PreferedNotificationMethod> PreferedNotificationMethods; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Address  
            modelBuilder.Entity<Address>()
                .HasKey(a => a.AddressId);
            modelBuilder.Entity<Address>()
                .HasMany(a => a.Customers)
                .WithOne(c => c.Address)
                .HasForeignKey(c => c.AddressId);

            // Customer  
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            // Product  
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);

            // Order  
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Claims)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            // OrderItem  
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.OrderItemId);

            // Claim  
            modelBuilder.Entity<Claim>()
                .HasKey(c => c.ClaimId);

            // NotificationMethod  
            modelBuilder.Entity<NotificationMethod>()
                .HasKey(nm => nm.NotificationMethodId);

            // Many-to-Many: Customer <-> NotificationMethod (via PreferedNotificationMethod)  
            modelBuilder.Entity<PreferedNotificationMethod>()
                .HasKey(pnm => new { pnm.CustomerId, pnm.NotificationMethodId });
            modelBuilder.Entity<PreferedNotificationMethod>()
                .HasOne(pnm => pnm.Customer)
                .WithMany(c => c.PreferedNotificationMethods)
                .HasForeignKey(pnm => pnm.CustomerId);
            modelBuilder.Entity<PreferedNotificationMethod>()
                .HasOne(pnm => pnm.NotificationMethod)
                .WithMany(nm => nm.PreferedNotificationMethods)
                .HasForeignKey(pnm => pnm.NotificationMethodId);
        }
    }
}
