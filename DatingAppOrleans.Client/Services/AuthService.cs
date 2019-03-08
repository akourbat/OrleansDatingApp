using DatingAppOrleans.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace DatingAppOrleans.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;

        public AuthService(HttpClient client, TokenService tokenService)
        {
            _client = client;
            _tokenService = tokenService;
        }

        private class TokenResponse
        {
            public string token;
        }

        public async Task LogInAsync(LoginUserCommand command)
        {
            var result = await _client.PostJsonAsync<TokenResponse>("/api/auth/Login", command);

            if (!string.IsNullOrEmpty(result.token))
            {
                await _tokenService.SaveAccessToken(result.token);
            }
        }

        public async Task RegisterAsync(RegisterUserCommand command)
        {
            await _client.PostJsonAsync("/api/auth/Register", command);
        }

    }
}
