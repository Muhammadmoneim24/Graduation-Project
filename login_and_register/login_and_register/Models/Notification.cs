using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        
        public ICollection<CourseNotification> CourseNotifications { get; set; } = new List<CourseNotification>();
        public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();


    }
}
