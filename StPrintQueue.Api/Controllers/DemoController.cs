using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StPrintQueue.Api.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        private readonly IApplicationLifetime _appLifeTime;

        public DemoController(IApplicationLifetime appLifeTime)
        {
            _appLifeTime = appLifeTime;
        }

        /*
            IIS Express has no graceful shutdown option.
            I created this controller with action "Stop" in order to demonstrate graceful shutdown (to save queue data).
            /api/demo/stop
        */

        [HttpGet]
        [Route("stop")]
        public void Stop()
        {
            _appLifeTime.StopApplication();

        }

    }
}
