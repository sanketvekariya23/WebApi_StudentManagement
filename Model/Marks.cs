using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Model
{
    public class Marks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]public int MId { get; set; }
        [ForeignKey("StudentId")]public Student Student { get; set; }
        [ForeignKey("SubjectId")] public Subject Subject { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        [Required] public int MarkObtained { get; set; }
    }
}