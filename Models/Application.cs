using System.ComponentModel.DataAnnotations.Schema;

namespace _1670WebApplication.Models
{
    public class Application
    {
        public int ID { get; set; }

        [ForeignKey("JobSeeker")]
        public int JobSeekerID { get; set; }

        [ForeignKey("JobList")]
        public int JobListingID { get; set; }

        public string Status { get; set; }
        public string Resume { get; set; }
        public string CoverLetter { get; set; }
        public string SelfIntroduction { get; set; }

        public virtual JobSeeker? JobSeeker { get; set; }
        public virtual JobList? JobList { get; set; }
    }
}
