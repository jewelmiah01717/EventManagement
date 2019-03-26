using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class Subscriber
    {
        [Key]
        public int ID { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }
       
    }
}