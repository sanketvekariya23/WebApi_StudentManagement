using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Model
{
    public class Subject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key] public int SId { get; set; }
        [Required]public string SubjectName { get; set; }
        [Required]public int SubjectId { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]public Student Student { get; set; }
    }
}
