using System.ComponentModel.DataAnnotations.Schema;

namespace login_and_register.Dtos
{
    public class CommentModel
    {
        public int DiscussionId { get; set; }

        public string ApplicationUserId { get; set; }
        public string Content { get; set; }
    }
}
