using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngularEshop.DataLayer.Entities.Access;
using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Entities.Orders;
using AngularEshop.DataLayer.Entities.Product;
using AngularEshop.DataLayer.Entities.Site;
using Microsoft.EntityFrameworkCore;

namespace AngularEshop.DataLayer.Context
{
    public class AngularEshopDbContext : DbContext
    {
        #region constructor
        public AngularEshopDbContext(DbContextOptions<AngularEshopDbContext> options) : base(options)
        {

        }
        #endregion

        #region Db Sets
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public DbSet<ProductVisit> ProductVisits { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion

        #region disable cascading delete in database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascades)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
