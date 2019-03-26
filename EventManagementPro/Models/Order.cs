using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        [Required, StringLength(50)]
        public string CustomerName { get; set; }
        [Required, StringLength(250)]
        public string ShippingAddress { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; }
        [StringLength(50)]
        public string TransactionId { get; set; }
        //pu
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}