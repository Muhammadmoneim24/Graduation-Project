using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class ApplicationUser   : IdentityUser
    {
        //public int Id { get;set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int SubmissionId { get; set; }
        public int DiscussionId { get; set; }

        public Submission Submission { get; set; }

        public Discussion Discussion { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<Comment> Comments { get; set; }




    }
}
