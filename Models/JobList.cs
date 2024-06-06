using System.ComponentModel.DataAnnotations.Schema;

namespace _1670WebApplication.Models
{
    public class JobList
    {
        public int ID { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        [ForeignKey("Employer")]
        public int EmployerID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Required { get; set; }
        public DateTime ApplicationDeadline { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Employer? Employer { get; set; }

        public ICollection<Application>? Applications { get; set; }
    }
}
