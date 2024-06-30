namespace login_and_register.Dtos
{
    public class GroupFileModel
    {
        public int GruopId { get; set; }
        public string SenderId { get; set; }

        public IFormFile? File {  get; set; }
    }
}
