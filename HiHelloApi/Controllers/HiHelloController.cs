using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HiHelloApi
{
    [Route("api/")]
    public class HiHelloController : Controller
    {
        // GET api/
        [HttpGet]
        public string Get()
        {
            return "type hi or hello in url";
        }

        // GET api/{string}
        [HttpGet("{msg}")]
        public string Get(string msg)
        {
            if (msg == "hi")
            {
                return "hello";
            }
            else if (msg == "hello")
            {
                return "hi";
            }
            else
            {
                return "error";
            }
        }
    }
}
