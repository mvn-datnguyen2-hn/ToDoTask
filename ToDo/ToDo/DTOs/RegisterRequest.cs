using System.ComponentModel.DataAnnotations;

namespace ToDo.DTO
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string  Username { get; set; }
        [Required]
        public string Password { get; set; }
        

    }
}
