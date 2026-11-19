using Application.Interfaces.Reposatoriese;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Reposatories
{
    public class CartRepo : ICartRepo
    {
        private readonly E_COmmerceDbContext db;

        public CartRepo(E_COmmerceDbContext db)
        {
            this.db = db;
        }

        public async Task AddToCart(CartItem item)
        {
            await db.CartItems.AddAsync(item);
        }

        public async Task CreateCart(Cart cart)
        {
            await db.Carts.AddAsync(cart);
        }

        public async Task Delete(CartItem item, string UserId)
        {
            var cartid = db.Carts.Where(cartid => cartid.UserId == UserId);
            db.CartItems.Remove(item);
        }
        public async Task<Cart?> GetCart( string UserId)
        {
            return await db.Carts.Where(cartid => cartid.UserId == UserId).FirstOrDefaultAsync();
            
        }

        public async Task<CartItem?> GetCartItemById(string cartitemId,string cartId)
        {
            return await db.CartItems.FirstOrDefaultAsync(c => c.Id == cartitemId&&c.CartId==cartId);
        }
        public async Task<CartItem> GetCartItemByProductId(string productId,string UserId)
        {
            return await db.CartItems.Include(c=>c.product).FirstOrDefaultAsync(c =>c.ProductId==productId&&c.cart!.UserId==UserId);
        }

        public async Task deleteAll(string userId)
        {
           var cartId= await db.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();
            var listcartitem=await db.CartItems.Where(c => c.CartId == cartId!.Id).ToListAsync();
            db.CartItems.RemoveRange(listcartitem);
        }

        public async Task<List<CartItem>> GetAll(string UserId)
        {
            return await  db.CartItems.Include(c=>c.product).Where(c => c.cart!.UserId == UserId).ToListAsync();
        }

        public async Task save()
        {
            await db.SaveChangesAsync();
        }

        public async Task Update(CartItem item)
        {
             db.CartItems.Update(item);
        }
    }
}
