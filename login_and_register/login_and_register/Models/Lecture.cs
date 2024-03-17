using System.ComponentModel.DataAnnotations.Schema;

namespace login_and_register.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }

        public byte[]? LecFile { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
