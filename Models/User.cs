namespace _1670WebApplication.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public ICollection<JobSeekers>? JobSeekers { get; set; }
        public ICollection<Employer>? Employers { get; set; }
        public ICollection<Admin>? Admins { get; set; }
    }
}
