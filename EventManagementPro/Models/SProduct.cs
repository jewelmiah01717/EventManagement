using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class SProduct
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        [Required, StringLength(30)]
        public string Category { get; set; }
        public string PictureFile { get; set; }
        public string Picture { get; set; }
        public int Stocklevel { get; set; }
        public virtual List<StockIn> StockIns { get; set; }
    }
}