using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class CourseNotification
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public int  NotificationId { get; set;}

        public string Content { get; set; }

        public Course Course { get; set; }
        public Notification Notification { get; set; }
    }
}
