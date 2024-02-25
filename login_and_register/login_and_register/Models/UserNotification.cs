using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class UserNotification
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public int NotificationId { get; set; }

        public string Content { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Notification Notification { get; set; }
    }
}
