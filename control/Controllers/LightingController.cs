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
        Lighting led = new Lighting();


        [HttpPost]
        public void ToggleLight([FromBody] Lighting lighting)
        {
            if(lighting.toggle == "true")
            {
                led.ledOn();
            }
            else if(lighting.toggle == "false")
            {
                led.ledOff();
            }
        }
    }
}
