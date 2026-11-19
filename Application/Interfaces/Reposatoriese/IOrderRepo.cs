namespace Application.Interfaces.Reposatoriese
{
    public interface IOrderRepo
    {
        public Task CreateOrder(Order Order);
        public Task<List<Order>> GetMyOrder(string UserId);
        public Task<List<OrderItem>> GetMyOrderItem(string OrderId);
        public Task UpdateStatusOrder(Order Order);
        public Task<Order> GetOrderById(string OrderId,string UserID);

        public Task save();
    }
}
