using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Device.Gpio;
using System.Net.Http;


namespace control.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LightingController : ControllerBase
    {
        [HttpPost]
        public void ToggleLight([FromBody] Lighting lighting)
        {
            lighting.setRgb();
        }
    }
}
