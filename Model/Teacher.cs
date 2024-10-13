using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Model
{
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]public int TeacherId { get; set; }
        [Required]public string TeacherName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string SubjectProfessor { get; set; }
    }
}
