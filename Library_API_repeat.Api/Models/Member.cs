namespace Library_API_repeat.Api.Models
{
    public class Member
    {
        public int id { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }

        public DateTime MembershipDate { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
