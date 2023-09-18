using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResumeManagement.Api.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string EmployeeId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}