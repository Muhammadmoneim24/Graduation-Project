using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int DiscussionId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public string Content { get; set; }

        public Discussion Discussion { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
    }
}
