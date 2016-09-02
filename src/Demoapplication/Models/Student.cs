using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Demoapplication.Model
{

    public class StudentDataBaseContext : DbContext {
        public StudentDataBaseContext(DbContextOptions<StudentDataBaseContext> options):base(options) { }
        public DbSet<StudentInformation> StudentInformation { get; set; }
    }
    public class StudentInformation
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Student Name")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(10)]
        [Required]
        [Display(Name="Contact No.")]
        public string ContactNo { get; set; }

        [Required]
        public decimal Marks { get; set; }

        [MaxLength(50)]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
    }
    
}
