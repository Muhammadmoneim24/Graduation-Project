using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public ICollection<CourseNotification> CourseNotifications { get; set; } = new List<CourseNotification>();

        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();



    }
}
