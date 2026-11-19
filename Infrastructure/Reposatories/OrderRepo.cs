using Application.Interfaces.Reposatoriese;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Reposatories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly E_COmmerceDbContext db;

        public OrderRepo(E_COmmerceDbContext db)
        {
            this.db = db;
        }
        public async  Task CreateOrder(Order Order)
        {
            await db.Orders.AddAsync(Order);
        }

        public async Task<List<Order>> GetMyOrder(string UserId)
        {
            return await db.Orders.Where(o=>o.UserId==UserId).ToListAsync();
        }
        public async Task<List<OrderItem>> GetMyOrderItem(string OrderId)
        {
            return await db.OrderItems.Where(o => o.OrderId == OrderId).ToListAsync();
        }
        public async Task UpdateStatusOrder(Order Order)
        {
            db.Orders.Update(Order);
        }
        public async Task<Order> GetOrderById(string OrderId, string UserID)
        {
            return await db.Orders.Where(o => o.Id == OrderId&&o.UserId==UserID).FirstOrDefaultAsync();
        }
        public async Task save()
        {
            await db.SaveChangesAsync();
        }
    }
}
