using System.ComponentModel.DataAnnotations;

namespace Schedular.API.DTO
{
    public class LoginUserDTO
    {
        //[Required]
        public string Username { get; set; }

        //[Required]
        //[StringLength(8, MinimumLength = 6, ErrorMessage = "Password should be between 6 and 8 characters.")]
        public string Password { get; set; }
    }
}