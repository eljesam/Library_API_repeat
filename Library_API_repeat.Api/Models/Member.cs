namespace Library_API_repeat.Api.Models
{
    public class Member
    {
        public int id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime MembershipDate { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
