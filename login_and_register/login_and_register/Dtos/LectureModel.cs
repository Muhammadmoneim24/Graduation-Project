using login_and_register.Models;
using System.Text.Json.Serialization;

namespace login_and_register.Dtos
{
    public class LectureModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }

        public IFormFile File { get; set; }


    }
}
