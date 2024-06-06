namespace _1670WebApplication.Models
{
    public class JobSeeker
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Resume { get; set; }
        public string CoverLetter { get; set; }

        public virtual User? User { get; set; }
        public ICollection<Application>? Applications { get; set; }
    }
}
