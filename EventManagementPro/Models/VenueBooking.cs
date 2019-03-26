using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagementPro.Models
{
    public class VenueBooking
    {
        public int VenueBookingID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        public string EventName { get; set; }
        public string EventID { get; set; }
        public DateTime EventDate { get; set; }
        public string HallName { get; set; }
        public int NumberofGuest { get; set; }


        public virtual Event Event { get; set; }
    }
}