using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingAppOrleans.Client.Services
{
    public class TokenService

    {
        private readonly IJSRuntime jsRuntime;

        public TokenService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public Task SaveAccessToken(string accessToken)
        {
            return jsRuntime.InvokeAsync<object>("wasmHelper.saveAccessToken", accessToken);
        }
        public Task<string> GetAccessToken()
        {
            return jsRuntime.InvokeAsync<string>("wasmHelper.getAccessToken");
        }

        public Task RemoveAccessToken()
        {
            return jsRuntime.InvokeAsync<object>("wasmHelper.removeAccessToken");
        }
    }
}
