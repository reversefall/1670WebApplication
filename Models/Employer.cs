using System.ComponentModel.DataAnnotations.Schema;

namespace _1670WebApplication.Models
{
    public class Employer
    {
        public int EmployerID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }

        public virtual User? User { get; set; }

        public ICollection<JobList>? JobLists { get; set; }
    }
}
