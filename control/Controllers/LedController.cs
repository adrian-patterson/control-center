using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
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
            var color = Color.FromArgb(ledColor.r, ledColor.b, ledColor.g);
            Program.SetAllLeds(color);
        }

        [Route("[controller]/Sequence")]
        [HttpPost]
        public void SetSequence([FromBody] LedSequence ledSequence)
        {
            if (ledSequence.sequence == "Rainbow") Program.Rainbow();
            if (ledSequence.sequence == "Carousel") Program.Carousel();
            if (ledSequence.sequence == "Rgb") Program.Rgb();
            if (ledSequence.sequence == "Jungle") Program.Jungle();
        }
    }
}
