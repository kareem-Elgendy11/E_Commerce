using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class E_COmmerceDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<ProductImages> ProductImages { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Order> Orders => Set<Order>();
        public virtual DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public E_COmmerceDbContext(DbContextOptions<E_COmmerceDbContext> options): base(options)
        {
            
        }
    }
}
