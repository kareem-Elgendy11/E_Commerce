using Application.Dtos.OrderDto;

namespace Application.Interfaces.IServices
{
    public interface IOrderService
    {
        public Task<ApiResponse<string>> CreateOrderAsync(string userId);
        public Task<ApiResponse<List<OrederDto>>> GetMyOrdersAsync(string userId);
        public Task<ApiResponse<string>> CancelOrderAsync(string orderId, string userId);
        public Task<ApiResponse<string>> MarkAsDelivered(string orderId, string userId);

    }
}
