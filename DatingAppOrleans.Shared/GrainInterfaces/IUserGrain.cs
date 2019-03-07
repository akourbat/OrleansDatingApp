using DatingAppOrleans.Shared.DTOs;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingAppOrleans.Shared.GrainInterfaces
{
    public interface IUserGrain: IGrainWithStringKey
    {
        Task<List<string>> GetUsers();

        Task<UserDto> RegisterAsync(RegisterUserCommand command);

        Task<UserDto> LoginAsync(LoginUserCommand command);
    }
}
