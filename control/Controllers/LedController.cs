using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace control.Controllers
{
    [ApiController]
    public class LedController : ControllerBase
    {
        [Route("[controller]/Color")]
        [HttpPost]
        public void SetRgb([FromBody] LedColor ledColor)
        {
            Task.Run(() =>
            {
                ledColor.SetRgb();
            }).Wait();
        }

        [Route("[controller]/Sequence")]
        [HttpPost]
        public void SetSequence([FromBody] LedSequence ledSequence)
        {
            Task.Run(() =>
            {
                ledSequence.ExecuteSequence();
            }).Wait();
            
        }
    }
}
