using DatingAppOrleans.Grains.DataAccess;
using DatingAppOrleans.Shared.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DatingAppOrleans.Grains.UserGrain
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly DatingContext context;

        public RegisterUserHandler(DatingContext context)
        {
            this.context = context;
        }

        async Task<UserDto> IRequestHandler<RegisterUserCommand, UserDto>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                UserName = request.UserName,
                State = UserState.Registered,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new UserDto { UserName = request.UserName };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
