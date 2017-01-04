using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

using Model;

namespace DAL
{
    public class InmallContext : IdentityDbContext<AppUser>
    {
        #region    User

        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }

        #endregion

        #region    Product

        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategorys { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<EthnicGroup> EthnicGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotoes { get; set; }
        public DbSet<CategoryPhoto> CategoryPhotoes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AD> Ads { get; set; }


        #endregion

        #region  Order
        public DbSet<CartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PersonInfo> PersonInfoes { get; set; }
        public DbSet<PayWay> PayWays { get; set; }
        public DbSet<ShippingRecord> ShippingRecords { get; set; }
        public DbSet<ShippingCompany> ShippingCompanies { get; set; }

        #endregion

        public InmallContext()
            : base("InmallContext", throwIfV1Schema: false)
        {

        }

        public static InmallContext Create()
        {
            return new InmallContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<Address>();

            base.OnModelCreating(modelBuilder);
        }


        public void Commit()
        {
            base.SaveChanges();
        }


    }
}
