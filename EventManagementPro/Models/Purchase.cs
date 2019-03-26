using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class Purchase
    {
        [Key]
        public int PurId { get; set; }
        [Display(Name = "Purchase Date")]
        public DateTime PurDate { get; set; }
        [Display(Name = "Purchase From")]
        public string PurFrom { get; set; }
        [Required]
        public int PurNo { get; set; }
        [Display(Name = "Book Name")]
        public string PurchaseProductName { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
    }
}