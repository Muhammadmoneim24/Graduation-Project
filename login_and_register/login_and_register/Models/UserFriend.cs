namespace login_and_register.Models
{
    public class UserFriend
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string FriendId { get; set; }
        public ApplicationUser Friend { get; set; }
    }
}
