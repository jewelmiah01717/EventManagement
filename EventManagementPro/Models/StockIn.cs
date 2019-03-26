using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class StockIn
    {
        public int StockInId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int quantity { get; set; }
        //Navigation
        [ForeignKey("SProduct")]
        public int SBookId { get; set; }
        public virtual SProduct SProduct { get; set; }
    }
}