using Application.Dtos.OrderDto;
using AutoMapper;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo repo;
        private readonly IMapper mapper;
        private readonly ICartRepo cartRepo;

        public OrderService(IOrderRepo repo, IMapper mapper,ICartRepo cartRepo)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cartRepo = cartRepo;
        }
        public async Task<ApiResponse<string>> CancelOrderAsync(string orderId, string userId)
        {
            try
            {
                var order = await repo.GetOrderById(orderId, userId);
                if (order == null)
                {
                    return ApiResponse<string>.Faild("Order Not Found");
                }
                if (order.Status != OrderStatus.Pending)
                {
                    return ApiResponse<string>.Faild("Cant Cancel Order");
                }
                order.Status = OrderStatus.Cancelled;
                await repo.UpdateStatusOrder(order);
                await repo.save();
                return ApiResponse<string>.Success("Order cancelled successfully");
            }
            catch(Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }
        }
        public async Task<ApiResponse<string>> MarkAsDelivered(string orderId, string userId)
        {
            try
            {
                var order = await repo.GetOrderById(orderId, userId);
                if (order == null)
                {
                    return ApiResponse<string>.Faild("Order Not Found");
                }
                if (order.Status != OrderStatus.Pending)
                {
                    return ApiResponse<string>.Faild("Cant Update Order");
                }
                order.Status = OrderStatus.Delivered;
                await repo.UpdateStatusOrder(order);
                await repo.save();
                return ApiResponse<string>.Success("Order Updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }
        }

        public async Task<ApiResponse<string>> CreateOrderAsync(string userId)
        {
            try
            {
                var cartitem = await cartRepo.GetAll(userId);
                if (cartitem == null || !cartitem.Any())
                {
                    return ApiResponse<string>.Faild("Cart is empty");
                }
                var order = new Domain.Models.Order
                {
                    UserId = userId,
                    TotalPrice = cartitem.Sum(x => x.TotalPrice * x.Quantity),
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.Now,
                    Items = cartitem.Select(x => new OrderItem
                    {
                        ProductId = x.ProductId,
                        ProductName = x.product!.Name,
                        Price = x.TotalPrice,
                        Quantity = x.Quantity
                    }).ToList()
                };
                await repo.CreateOrder(order);
                await cartRepo.deleteAll(userId);
                await repo.save();
                return ApiResponse<string>.Success("Order created successfully");
            }
            catch(Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }
        }

        public async Task<ApiResponse<List<OrederDto>>> GetMyOrdersAsync(string userId)
        {
            try
            {
                var orders = await repo.GetMyOrder(userId);
                var orderDtos = mapper.Map<List<OrederDto>>(orders);
                return ApiResponse<List<OrederDto>>.Success(orderDtos);
            }
            catch(Exception ex)
            {
             return ApiResponse<List<OrederDto>>.Faild(ex.InnerException!.Message);

            }
        }
    }
}
