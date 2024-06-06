using System.ComponentModel.DataAnnotations.Schema;

namespace _1670WebApplication.Models
{
    public class Admin
    {
        public int ActionID { get; set; }

        [ForeignKey("User")]
        public int AdminID { get; set; }

        public string ActionType { get; set; }
        public string ActionDetail { get; set; }

        public virtual User? User { get; set; }
    }
}
