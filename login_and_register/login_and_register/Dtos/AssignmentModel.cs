using login_and_register.Models;
using System.Text.Json.Serialization;

namespace login_and_register.Dtos
{
    public class AssignmentModel
    {
        public string Tittle { get; set; }
        public string? Description { get; set; }
        public int Grade { get; set; }
        public string? EndDate { get; set; }

        public IFormFile? File { get; set; }

    }
}
