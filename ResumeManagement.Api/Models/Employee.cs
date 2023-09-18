using System.ComponentModel.DataAnnotations;

namespace ResumeManagement.Api.Models
{
    public class Employee
    {

        [Key]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
        public bool IsWorking { get; set; }
        public string ImageUrl { get; set; }
        public List<Experience> Experiences { get; set; }
    }
}
