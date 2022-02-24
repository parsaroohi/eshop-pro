using System;
using System.Collections.Generic;
using System.Text;
using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Entities.Common;

namespace AngularEshop.DataLayer.Entities.Orders
{
    public class Order : BaseEntity
    {
        #region properties
        public long UserId { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        #endregion

        #region relations
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        #endregion
    }
}
