using System.ComponentModel.DataAnnotations;
namespace Library_API_repeat.Client.Models.Members
{
    public class UpdateMemberRequest
    {
        [Required]
        public DateTime MembershipDate { get; set; }
    }
}
