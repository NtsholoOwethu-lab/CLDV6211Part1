using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    [Table("Venue")]
    public partial class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(250)]
        public string? VenueName { get; set; }

        [Required]
        [StringLength(250)]
        public string? Location { get; set; }

        public int Capacity { get; set; }

        [Required]
        [StringLength(250)]
        public string? ImageUrl {  get; set; }

        public List<Bookings> Bookings { get; set; } = new();
    }
}
