using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DataAccessLayer
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        private readonly string _windowsConnectionString = @"Server=.\SQLExpress;Database=ProjectDatabase1;Trusted_Connection=True;TrustServerCertificate=true";

        public DbSet<TestModel> TestModels { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(_windowsConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //one-to-many intre produs si review
            builder.Entity<Product>()
            .HasMany(s => s.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);

            builder.Entity<Cart>(x=>x.HasKey(p=>new {p.AppUserId, p.ProductId}));

            builder.Entity<Cart>()
                .HasOne(u=>u.AppUser)
                .WithMany(u=>u.Carts)
                .HasForeignKey(u=>u.AppUserId);
           
            builder.Entity<Cart>()
                .HasOne(u => u.Product)
                .WithMany(u => u.Carts)
                .HasForeignKey(u => u.ProductId);


            List<IdentityRole> roles = new List<IdentityRole> {
               new IdentityRole
               {
                   Name="Admin",
                   NormalizedName="ADMIN"
               },
               new IdentityRole
               {
                   Name="User",
                   NormalizedName="USER"
               },
            };
            builder.Entity<IdentityRole>().HasData(roles);



        }

    }
}
