using DatingAppOrleans.Shared.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingAppOrleans.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly IClusterClient client;

        public SampleDataController(IClusterClient client)
        {
            this.client = client;
        }

        [HttpGet("[action]")]
        public async Task<List<string>> GetUsers()
        {

            var grain = client.GetGrain<IUserGrain>("blahblah");
            return await grain.GetUsers();
        }
    }
}
