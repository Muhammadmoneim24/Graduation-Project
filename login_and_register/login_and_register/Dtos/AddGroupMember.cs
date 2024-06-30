namespace login_and_register.Dtos
{
    public class AddGroupMember
    {
        public int GroupID { get; set; }
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }
    }
}
