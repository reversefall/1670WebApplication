namespace _1670WebApplication.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public ICollection<JobList>? JobLists { get; set; }
    }
}
