using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pro_WebApiLayer.Controllers
{
    [Authorize(Roles ="admin,customer,guest")]
     public class CarWebApiServiceController : ApiController
    {


    }
}
