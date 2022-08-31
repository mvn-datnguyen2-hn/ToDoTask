using System.ComponentModel.DataAnnotations;

namespace ToDo.DTO
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
