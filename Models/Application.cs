using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
 
namespace _1670WebApplication.Models
{
    public class Application
    {
        public int ID { get; set; }

        [ForeignKey("JobSeeker")]
        public int JobSeekersID { get; set; }

        [ForeignKey("JobList")]
        public int JobListID { get; set; }

        public string Status { get; set; }
        public string? Resume { get; set; }
        public string CoverLetter { get; set; }
        public string SelfIntroduction { get; set; }

        public virtual JobSeekers? JobSeeker { get; set; }
        public virtual JobList? JobList { get; set; }

        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

    }
}
