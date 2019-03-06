using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DatingAppOrleans.Shared.DTOs
{
    public class RegisterUserCommand: IRequest<UserDto>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}
