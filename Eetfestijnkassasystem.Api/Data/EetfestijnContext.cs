using Microsoft.EntityFrameworkCore;
using Eetfestijnkassasystem.Shared.Model;

namespace Eetfestijnkassasystem.Api.Data
{
    public class EetfestijnContext : DbContext
    {
        public EetfestijnContext (DbContextOptions<EetfestijnContext> options) : base(options)
        {
            
        }

        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Event> Event { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ignore non-serialized properties
            //modelBuilder.Entity<Order>().Ignore(o => o.Items);
            //modelBuilder.Entity<Order>().Ignore(o => o.IsPaid);
            //modelBuilder.Entity<Order>().Ignore(o => o.TotalCost);
            //modelBuilder.Entity<MenuItem>().Ignore(o => o.Orders);

            // configure many-to-many rel. between Order and MenuItem via "join/bridging" table OrderMenuItem,
            // and ensure when Order or MenuItem is deleted, so are related entries in OrderMenuItem table.
            modelBuilder.Entity<OrderMenuItem>()
                        .HasKey(omi => new { omi.OrderId, omi.MenuItemId });

            modelBuilder.Entity<OrderMenuItem>()
                        .HasOne(omi => omi.Order)
                        .WithMany(b => b.OrderMenuItems)
                        .HasForeignKey(bc => bc.OrderId);

            modelBuilder.Entity<OrderMenuItem>()
                        .HasOne(omi => omi.MenuItem)
                        .WithMany(b => b.OrderMenuItems)
                        .HasForeignKey(bc => bc.MenuItemId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
