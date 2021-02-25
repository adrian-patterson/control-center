using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Device.Gpio;
using System.Net.Http;


namespace control.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LightingController : ControllerBase
    {
        Lighting led = new Lighting();

        [HttpPost]
        public void lightOn()
        {
            led.toggleLight();
        }
    }
}
