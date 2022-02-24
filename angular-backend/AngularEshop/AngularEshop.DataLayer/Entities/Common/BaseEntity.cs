using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AngularEshop.DataLayer.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public Boolean IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
