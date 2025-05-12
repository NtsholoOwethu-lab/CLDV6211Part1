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
        
        public string? Location { get; set; }

         [Required]
          [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        
        
        //This stays - to store the URL of the image uploaded
        public string? ImageUrl { get; set; }

        //Add this new one - only for uploading from Create/Edit form
        [NotMapped]
        public IFormFile? ImageFile { get; set; }


        public List<Bookings> Bookings { get; set; } = new();

        
    }
}
