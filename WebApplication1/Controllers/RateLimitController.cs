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
    public class RateLimitController : ControllerBase
    {
        private static DateTime previousTimePoint = DateTime.Now;
        private static Hashtable IpCountHt = new Hashtable();

        private readonly ILogger<VotingController> _logger;

        public RateLimitController(ILogger<VotingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            DateTime requestTime = DateTime.Now;
            TimeSpan ts = requestTime - previousTimePoint;

            if(ts.TotalSeconds > 60)
            {
                previousTimePoint = requestTime;
                IpCountHt.Clear();
            }
            

            var requestIp = Request.HttpContext.Connection.RemoteIpAddress;
            if(IpCountHt.Contains(requestIp))
            {
                var count = IpCountHt[requestIp] as int?;
                IpCountHt[requestIp] = ++count;
            }
            else
            {
                IpCountHt.Add(requestIp, 1);
            }


            if((int)IpCountHt[requestIp] >60)
            {
                return $"maximum request limit reached, please hold for {(previousTimePoint.AddMinutes(1) - requestTime):ss} seconds";
            }
            return $"request received, {IpCountHt[requestIp]}/60 since {previousTimePoint}";
        }
    }
}
