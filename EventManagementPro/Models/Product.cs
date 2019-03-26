using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
                    
        public int AvilableProduct { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string BookStatus { get; set; }
        public virtual Category Category { get; set; }
        

       
    }
}