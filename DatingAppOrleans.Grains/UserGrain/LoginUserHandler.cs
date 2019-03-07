using DatingAppOrleans.Grains.DataAccess;
using DatingAppOrleans.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DatingAppOrleans.Grains.UserGrain
{
    class LoginUserHandler : IRequestHandler<LoginUserCommand, UserDto>
    {
        private readonly DatingContext _context;

        public LoginUserHandler(DatingContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user == null)
                return null;

            if (!VerifyPasswodHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return new UserDto { Id = user.Id, UserName = user.UserName };
        }

        private bool VerifyPasswodHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
    }
}
