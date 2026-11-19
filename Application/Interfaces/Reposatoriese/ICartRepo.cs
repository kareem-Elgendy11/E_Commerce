using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Reposatoriese
{
    public interface ICartRepo
    {
        public Task CreateCart(Cart cart);
        public Task<List<CartItem>> GetAll(string UserId);
        public Task Update(CartItem item);
        public Task AddToCart(CartItem item);
        public Task deleteAll(string UserId);
        public Task Delete(CartItem item, string UserId);
        public Task<CartItem?> GetCartItemById(string cartitemId,string cartId);
        public Task<Cart?> GetCart(string UserId);
        public Task<CartItem> GetCartItemByProductId(string productId,string UserId);
        public Task save();
    }
}
