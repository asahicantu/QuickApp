// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DAL.Models.Interfaces;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public string CurrentUserId { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Svc> Svcs { get; set; }



        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string priceDecimalType = "decimal(18,2)";

            builder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.Name);
            builder.Entity<Customer>().Property(c => c.Email).HasMaxLength(100);
            builder.Entity<Customer>().Property(c => c.PhoneNumber).IsUnicode(false).HasMaxLength(30);
            builder.Entity<Customer>().Property(c => c.City).HasMaxLength(50);
            builder.Entity<Customer>().ToTable($"App{nameof(this.Customers)}");

            builder.Entity<ProductCategory>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<ProductCategory>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<ProductCategory>().ToTable($"App{nameof(this.ProductCategories)}");

            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Product>().HasIndex(p => p.Name);
            builder.Entity<Product>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<Product>().Property(p => p.Icon).IsUnicode(false).HasMaxLength(256);
            builder.Entity<Product>().HasOne(p => p.Parent).WithMany(p => p.Children).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>().ToTable($"App{nameof(this.Products)}");
            builder.Entity<Product>().Property(p => p.BuyingPrice).HasColumnType(priceDecimalType);
            builder.Entity<Product>().Property(p => p.SellingPrice).HasColumnType(priceDecimalType);

            builder.Entity<Order>().Property(o => o.Comments).HasMaxLength(500);
            builder.Entity<Order>().ToTable($"App{nameof(this.Orders)}");
            builder.Entity<Order>().Property(p => p.Discount).HasColumnType(priceDecimalType);

            builder.Entity<OrderDetail>().ToTable($"App{nameof(this.OrderDetails)}");
            builder.Entity<OrderDetail>().Property(p => p.UnitPrice).HasColumnType(priceDecimalType);
            builder.Entity<OrderDetail>().Property(p => p.Discount).HasColumnType(priceDecimalType);




            builder.Entity<Svc>(e =>
            {
                e.HasKey(c => new { c.Id });
                e.Property(p => p.Id).IsRequired(true);
                e.Property(p => p.DomainId).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.AccountId).HasMaxLength(250).IsRequired(false);

                e.Property(p => p.ServiceDeliveryManager).HasMaxLength(20).IsRequired(false);
                e.Property(p => p.Country).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.ServiceDeliveryManager).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.AccountManager).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.AccountManager).HasMaxLength(20).IsRequired(false);
                //e.Property(p => p.Date).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.QuoteFTL).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.PO).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.Client).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.Field).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.Well).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.AU).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.AC).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.Portfolio).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.SubPortfolio).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.MasterCode).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.Currency).HasMaxLength(5).IsRequired(false);
                e.Property(p => p.FXRate).HasColumnType("NUMERIC(6,2)").IsRequired(false);
                e.Property(p => p.Comment).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.TechnicalLead).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.ChangePointTask).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.ROFO).HasColumnType("MONEY").IsRequired(false);
                e.Property(p => p.iMF).HasColumnType("MONEY").IsRequired(false);
                e.Property(p => p.MMF).HasColumnType("MONEY").IsRequired(false);
                e.Property(p => p.SentToInvoice).HasColumnType("MONEY").IsRequired(false);
                e.Property(p => p.Revenue).HasColumnType("MONEY").IsRequired(false);
                e.Property(p => p.InvocieNumber).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.Cost).HasColumnType("MONEY").IsRequired(false);
                e.Property(p => p.CostReceived).HasColumnType("MONEY").IsRequired(false);
                e.Property(p => p.CostType).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.GLAccount).HasMaxLength(250).IsRequired(false);
                e.Property(p => p.CostDescription).HasMaxLength(250).IsRequired(false);
            });

            }




        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                DateTime now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = CurrentUserId;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = CurrentUserId;
            }
        }
    }
}
