using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Model
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]public int StudentId { get; set; }
        [Required]public string StudentName { get; set; }
        [Required] public int EnrollmentNo { get; set; }
        [Required] public string Address { get; set; }  
        [Required] public DateTime BirthDate { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Marks> Marks { get; set; }
    }
}
