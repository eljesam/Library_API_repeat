namespace Library_API_repeat.Client.Models.Members
{
    public class Member
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime MembershipDate { get; set; }
    }
}
