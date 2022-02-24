using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Core.DTOs.Orders;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.DataLayer.Entities.Orders;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace AngularEshop.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region constructor
        private readonly IGenericRepository<Order> orderRepository;
        private readonly IGenericRepository<OrderDetail> orderDetailRepository;
        private readonly IUserService userService;
        private readonly IProductService productService;
        public OrderService(IGenericRepository<Order> orderRepository,
            IGenericRepository<OrderDetail> orderDetailRepository,
            IUserService userService,
            IProductService productService
            )
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.userService = userService;
            this.productService = productService;
        }

        #endregion

        #region order

        public async Task<Order> CreateUserOrder(long userId)
        {
            var order = new Order
            {
                UserId = userId
            };
            await orderRepository.AddEntity(order);
            await orderRepository.SaveChanges();
            return order;
        }

        public async Task<Order> GetUserOpenOrder(long userId)
        {
            var order = await orderRepository.GetEntitiesQuery()
                .Include(s => s.OrderDetails)
                .ThenInclude(s => s.Product)
                .SingleOrDefaultAsync(s => s.UserId == userId && !s.IsPaid && !s.IsDelete);

            if (order == null)
            {
                order = await CreateUserOrder(userId);
            }

            return order;
        }


        public async Task AddProductToOrder(long userId, long productId, int count)
        {
            var user = await userService.GetUserById(userId);
            var product = await productService.GetProductForUserOrder(productId);
            if (user != null && product != null)
            {
                var order = await GetUserOpenOrder(userId);
                if (count < 1) count = 1;
                var details = await GetOrderDetails(order.Id);
                var ExistsDetail = details.SingleOrDefault(p => p.ProductId == productId && !p.IsDelete);
                if (ExistsDetail != null)
                {
                    ExistsDetail.Count += count;
                    orderDetailRepository.UpdateEntity(ExistsDetail);
                }
                else
                {
                    var detail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = productId,
                        Count = count,
                        Price = product.Price
                    };
                    await orderDetailRepository.AddEntity(detail);
                }


                await orderDetailRepository.SaveChanges();
            }
        }
        #endregion

        #region order detail
        public async Task<List<OrderDetail>> GetOrderDetails(long orderId)
        {
            return await orderDetailRepository.GetEntitiesQuery()
                .Where(s => s.OrderId == orderId).ToListAsync();
        }

        public async Task<List<OrderBasketDetail>> GetUserBasketDetails(long userId)
        {
            var openOrder = await GetUserOpenOrder(userId);
            if (openOrder == null)
            {
                return null;
            }
            return openOrder.OrderDetails.Where(s => !s.IsDelete).Select(f => new OrderBasketDetail
            {
                Id = f.Id,
                Count = f.Count,
                Price = f.Price,
                Title = f.Product.ProductName,
                ImageName = PathTools.Domain + PathTools.ProductImagePath + f.Product.ImageName
            }).ToList();
        }
        public async Task DeleteOrderDetail(OrderDetail detail)
        {
            orderDetailRepository.RemoveEntity(detail);
            await orderDetailRepository.SaveChanges();
        }
        #endregion

        #region dispose

        public void Dispose()
        {
            orderRepository.Dispose();
            orderDetailRepository.Dispose();
        }
        #endregion
    }
}
