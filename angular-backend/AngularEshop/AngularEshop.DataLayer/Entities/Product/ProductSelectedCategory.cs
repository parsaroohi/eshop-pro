using System;
using System.Collections.Generic;
using System.Text;
using AngularEshop.DataLayer.Entities.Common;

namespace AngularEshop.DataLayer.Entities.Product
{
    public class ProductSelectedCategory : BaseEntity
    {
        #region properties
        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }
        #endregion

        #region relations
        public ProductCategory ProductCategory { get; set; }
        #endregion
    }
}
