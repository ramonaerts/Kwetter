using AuthenticationService.Models;

namespace AuthenticationService.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public UserRole Role { get; set; }
    }
}
