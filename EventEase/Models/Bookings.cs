

using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Bookings
    {
        [Key]
        public int BookingId { get; set; }

        public int VenueId { get; set; }

        public Venue? Venue { get; set; }

        public int EventId { get; set; }  // Foreign key to EventDetails (match the database column name)
        public EventDetails EventDetails { get; set; }


        public DateTime BookingDate { get; set; }


       

        //public int HoursLogged { get; set; } = new();


        //public string SupervisorFeedback { get; set; } = "";
    }
}
