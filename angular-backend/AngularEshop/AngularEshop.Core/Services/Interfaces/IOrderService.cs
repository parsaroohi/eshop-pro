using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Core.DTOs.Orders;
using AngularEshop.DataLayer.Entities.Orders;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IOrderService : IDisposable
    {
        #region order
        Task<Order> CreateUserOrder(long userId);
        Task<Order> GetUserOpenOrder(long userId);
        #endregion

        #region order detail
        Task AddProductToOrder(long userId, long productId, int count);
        Task<List<OrderDetail>> GetOrderDetails(long orderId);
        Task<List<OrderBasketDetail>> GetUserBasketDetails(long userId);
        Task DeleteOrderDetail(OrderDetail detail);
        #endregion
    }
}
