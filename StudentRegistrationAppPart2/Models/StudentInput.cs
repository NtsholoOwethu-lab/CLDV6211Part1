using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationAppPart2.Models
{
    [Table("StudentInput")]
    public partial class StudentInput
    {
        [Key] 
        public int StudentID { get; set; }
        [Required]
        [StringLength(250)]

        public string Name { get; set; }
        [Required]
        [StringLength(250)]

        public string Surname { get; set; }
        [Required]
        [StringLength(250)]

        public string Email { get; set; }

    }
}