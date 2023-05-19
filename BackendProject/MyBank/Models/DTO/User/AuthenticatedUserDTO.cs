
using System.Text.Json.Serialization;

namespace Models.DTO.User
{
    public class AuthenticatedUserDTO
    {
        [JsonIgnore]
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
