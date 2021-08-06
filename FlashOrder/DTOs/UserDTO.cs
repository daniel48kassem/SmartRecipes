using System.ComponentModel.DataAnnotations;

namespace FlashOrder.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        [Required]
        public string DisplayName { get; set; }
        
        [Required]
        public string Role { get; set; }
    }

    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    
    public class RegisterDTO:UserDTO
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}