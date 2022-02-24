using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularEshop.Core.DTOs.Products;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.Core.Utilities.Extensions.Identity;
using AngularEshop.DataLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularEshop.WebApi.Controller
{
    public class ProductsController : SiteBaseController
    {
        #region constructor
        private IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        #endregion

        #region products
        [HttpGet("filter-products")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDTO filter)
        {
            var products = await productService.FilterProducts(filter);
            return JsonResponseStatus.Success(products);
        }

        #endregion

        #region get products category
        [HttpGet("product-active-categories")]
        public async Task<IActionResult> GetProductsCategories()
        {
            return JsonResponseStatus.Success(await productService.GetAllAcitveProductCategories());
        }

        #endregion

        #region get sigle product
        [HttpGet("sigle-product/{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await productService.GetProductById(id);
            var productGallery = await productService.GetProductActiveGalleries(id);
            if (product != null)
                return JsonResponseStatus.Success(new { product = product, galleries = productGallery });

            return JsonResponseStatus.NotFound();
        }
        #endregion

        #region related products
        [HttpGet("related-products/{id}")]
        public async Task<IActionResult> GetRelatedProducts(long id)
        {
            var relatedProducts = productService.GetRelatedProducts(id);
            return JsonResponseStatus.Success(relatedProducts);
        }
        #endregion

        #region product comments
        [HttpGet("product-comments/{id}")]
        public async Task<IActionResult> GetProductComments(long id)
        {
            var comments = await productService.GetActiveProductComments(id);
            return JsonResponseStatus.Success(comments);
        }
        [HttpPost("add-product-comment")]
        public async Task<IActionResult> AddProductComment([FromBody] AddProductCommentDTO comment)
        {
            if (!User.Identity.IsAuthenticated)
                return JsonResponseStatus.Error(new { message = "لطفا ابتدا وارد سایت شوید" });

            if (!await productService.IsExistsProductById(comment.ProductId))
                return JsonResponseStatus.NotFound();

            var userId = User.GetUserId();
            var res = await productService.AddProductComment(comment, userId);
            return JsonResponseStatus.Success(res);
        }
        #endregion
    }
}
