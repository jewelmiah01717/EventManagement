using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("SProduct")]
        public int SBookId { get; set; }
        public int Quantity { get; set; }
        //
        public virtual Order Order { get; set; }
        public virtual SProduct SProduct { get; set; }
    }
}