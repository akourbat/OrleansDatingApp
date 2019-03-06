using DatingAppOrleans.Shared.DTOs;
using DatingAppOrleans.Shared.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingAppOrleans.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IClusterClient _client;

        public AuthController (IClusterClient client)
        {
            _client = client;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            command.UserName = command.UserName.ToLower();

            var userGrain = _client.GetGrain<IUserGrain>(command.UserName);
            var user = await userGrain.RegisterAsync(command);
            if (user == null) return BadRequest("User already exists");

            return StatusCode(201);
        }
    }
}
