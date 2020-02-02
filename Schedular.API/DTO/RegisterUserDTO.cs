using System;
using System.ComponentModel.DataAnnotations;

namespace Schedular.API.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "Password should be between 6 and 8 characters.")]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }

        public RegisterUserDTO()
        {
            Created = DateTime.Now;
        }
    }
}
