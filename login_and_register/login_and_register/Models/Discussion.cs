using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public string Tittle { get; set; }
        public string Content { get; set; }

        public byte[]? File { get; set; }
        public Course Course { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
