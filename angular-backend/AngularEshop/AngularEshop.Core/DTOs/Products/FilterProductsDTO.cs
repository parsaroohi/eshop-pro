using System;
using System.Collections.Generic;
using System.Text;
using AngularEshop.Core.DTOs.Paging;
using AngularEshop.DataLayer.Entities.Product;

namespace AngularEshop.Core.DTOs.Products
{
    public class FilterProductsDTO : BasePaging
    {
        public string Title { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public List<Product> Products { get; set; }
        public List<long> Categories { get; set; }
        public ProductOrderBy? OrderBy { get; set; }
        public FilterProductsDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;
            this.TakeEntity = paging.TakeEntity;
            return this;
        }
        public FilterProductsDTO SetProducts(List<Product> products)
        {
            this.Products = products;
            return this;
        }

        public enum ProductOrderBy
        {
            PriceAsc,
            PriceDec
        }
    }
}
