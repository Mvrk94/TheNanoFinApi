using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NanofinAPI.Controllers
{
    [Authorize]
    public class TestController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
