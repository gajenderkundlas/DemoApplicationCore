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
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        [Required]
        public string ContactNo { get; set; }
        public decimal Marks { get; set; } = 0;
        [MaxLength(50)]
        public string FatherName { get; set; } = "myname";
    }
    
}
