using Microsoft.AspNetCore.Mvc;
using System;

namespace control.Controllers
{
    [ApiController]
    public class LedController : ControllerBase
    {
        [Route("[controller]/Color")]
        [HttpPost]
        public void SetRgb([FromBody] LedColor ledColor)
        {
            ledColor.SetRgb();
        }

        [Route("[controller]/Sequence")]
        [HttpPost]
        public void SetSequence([FromBody] LedSequence ledSequence)
        {
            ledSequence.ExecuteSequence();
        }
    }
}
