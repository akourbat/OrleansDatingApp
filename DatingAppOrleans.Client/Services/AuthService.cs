using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DatingAppOrleans.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;

        public AuthService(HttpClient client)
        {
            _client = client;
        }
        



    }
}
