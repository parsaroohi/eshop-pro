using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AngularEshop.Core.DTOs.Products
{
    public class AddProductCommentDTO
    {
        public long ProductId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نباید بیشتر از {1} باشد")]
        public string Text { get; set; }
    }
}
