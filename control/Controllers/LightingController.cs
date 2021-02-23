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
        private readonly int ledPin = 11;
        private GpioController controller = new GpioController();
        private static readonly HttpClient client = new HttpClient();
        
        [HttpPost]
        public void turnOnLed()
        {
            controller.Write(ledPin, PinValue.High);
        }
        public LightingController()
        {
            controller.OpenPin(ledPin, PinMode.Output);
        }
    }
}
