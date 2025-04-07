using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StudentRegistrationAppPart2.Models
{
    public partial class RegistrationDBContext2 : DbContext
    {
        public RegistrationDBContext2()
            : base("name=RegistrationDBContext2")
        {
        }

        public virtual DbSet<StudentInput> Students { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentInput>()
                .HasKey(e => e.StudentID);
                

            modelBuilder.Entity<StudentInput>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<StudentInput>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<StudentInput>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
