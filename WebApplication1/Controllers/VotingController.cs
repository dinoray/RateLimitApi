using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VotingController : ControllerBase
    {
        private static int agree;
        private static int disagree;

        private readonly ILogger<VotingController> _logger;

        public VotingController(ILogger<VotingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return $"is Wuhan Coronavirus created from China? Agree:{agree}, Disagree:{disagree}";
        }

        [HttpPost]
        public string Vote([FromForm] bool isAgree)
        {

            if (isAgree)
                agree++;
            else
                disagree++;

            return  $"ErrorCode : 0, voting: {isAgree}";
        }
    }
}
