using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class EventDetails
    {
        [Key]
        public int EventId { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public string Descripion { get; set; }

        public int VenueId { get; set; }

        public Venue? Venue { get; set; } 


        public List<Bookings> Bookings { get; set; } = new();
    }
}
