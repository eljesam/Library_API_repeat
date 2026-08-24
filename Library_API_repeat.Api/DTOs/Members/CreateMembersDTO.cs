namespace Library_API_repeat.Api.DTOs.Members
{
    public class CreateMembersDTO
    {

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime MembershipDate { get; set; }
    }
}
