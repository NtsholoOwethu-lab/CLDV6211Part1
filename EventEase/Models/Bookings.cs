using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Bookings
    {
        [Key]
        public int BookingId { get; set; }

        public int VenueId { get; set; }
        public Venue? Venue { get; set; }

        [ForeignKey("EventDetails")]
        public int EventId { get; set; }
        public EventDetails? EventDetails { get; set; }

        public DateTime BookingDate { get; set; }
    }

}
