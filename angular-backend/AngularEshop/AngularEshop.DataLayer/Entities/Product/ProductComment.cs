using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Entities.Common;

namespace AngularEshop.DataLayer.Entities.Product
{
    public class ProductComment : BaseEntity
    {
        #region properties
        public long ProductId { get; set; }
        public long UserId { get; set; }
        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نباید بیشتر از {1} باشد")]
        public string Text { get; set; }
        #endregion

        #region relations
        public Product Product { get; set; }
        public User User { get; set; }
        #endregion
    }
}
