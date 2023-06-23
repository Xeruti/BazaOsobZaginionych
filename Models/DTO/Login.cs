using System.ComponentModel.DataAnnotations;

namespace BazaOsobZaginionych.Models.DTO
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
