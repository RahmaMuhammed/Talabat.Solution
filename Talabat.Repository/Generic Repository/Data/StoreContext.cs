using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Entities;

namespace Talabat.Repository.Generic_Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///  modelBuilder.ApplyConfiguration(new ProductConfiguration());
            ///  modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            ///  modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

    }
}