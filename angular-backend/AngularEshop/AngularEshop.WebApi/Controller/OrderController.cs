using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.Core.Utilities.Extensions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularEshop.WebApi.Controller
{
    public class OrderController : SiteBaseController
    {
        #region constructor
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        #endregion

        #region add product to order
        [HttpGet("add-order")]
        public async Task<IActionResult> AddProductToOrder(long productId, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();
                await orderService.AddProductToOrder(userId, productId, count);
                return JsonResponseStatus.Success(new
                {
                    message = "محصول با موفقیت به سبد خرید شما افزوده شد",
                    data = await orderService.GetUserBasketDetails(userId)
                });
            }
            return JsonResponseStatus.Error(new { message = "برای افزودن محصول به سبد خرید ابتدا لاگین کنید" });
        }
        #endregion

        #region user basket details
        [HttpGet("get-order-details")]
        public async Task<IActionResult> GetUserBasketDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                var details = await orderService.GetUserBasketDetails(User.GetUserId());
                return JsonResponseStatus.Success(details);
            }

            return JsonResponseStatus.Error();
        }
        #endregion

        #region remove order detail from basket
        [HttpGet("remove-order-details/{detailId}")]
        public async Task<IActionResult> RemoveOrderDetail(long detailId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userOpenOrder = await orderService.GetUserOpenOrder(User.GetUserId());
                var detail = userOpenOrder.OrderDetails.SingleOrDefault(i => i.Id == detailId);
                if (detail != null)
                {
                    await orderService.DeleteOrderDetail(detail);
                    return JsonResponseStatus.Success(
                        await orderService.GetUserBasketDetails(User.GetUserId())
                        );
                }
            }
            return JsonResponseStatus.Error();
        }
        #endregion
    }
}
