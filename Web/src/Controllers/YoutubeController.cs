// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc;

namespace CodeRabbits.KaoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public YoutubeController(
            IServiceScopeFactory serviceScopeFactory

            )
        {
            _serviceScopeFactory = serviceScopeFactory;
        }


    }
}
