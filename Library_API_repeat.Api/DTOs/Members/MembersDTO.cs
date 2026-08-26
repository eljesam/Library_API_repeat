namespace Library_API_repeat.Api.DTOs.Members
{
    public class MembersDTO
    {
        public int id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime MembershipDate { get; set; }
    }
}
