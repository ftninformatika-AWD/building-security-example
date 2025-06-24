using System.ComponentModel.DataAnnotations;

namespace BuildingExample.DTOs
{
    public class RegistrationDTO
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
