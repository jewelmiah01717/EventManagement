using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        
        public int AvilableProduct { get; set; }
        
        public string  Category { get; set; }
        public string ProductStatus { get; set; }

     
    }
}