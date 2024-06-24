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

        

        public ICollection<SubmissionAssignment> SubmissionAssignments { get; set; }

        public ICollection <Discussion> Discussions { get; set; }

        public ICollection<Submission> Submissions { get; set; }


        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ChatMessage> SentMessages { get; set; }
        public virtual ICollection<ChatMessage> ReceivedMessages { get; set; }
        public virtual ICollection<UserFriend> Friends { get; set; }
        public virtual ICollection<UserFriend> FriendOf { get; set; }
        public virtual ICollection<ChatGroup> OwnedGroups { get; set; }
        public virtual ICollection<ChatGroupMember> GroupMembers { get; set; }

    }
}
