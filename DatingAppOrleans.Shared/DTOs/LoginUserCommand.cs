using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DatingAppOrleans.Shared.DTOs
{
    public class LoginUserCommand : IRequest<UserDto>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
