using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public  class EventDetails
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(250)]
        public string EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(250)]
        public string Descripion { get; set; }

        
        [Required]
        public int VenueId { get; set; }

        public Venue? Venue { get; set; } 


        public List<Bookings> Bookings { get; set; } = new();
    }
}
