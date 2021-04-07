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

            Program.KillAllThreads();
            Program.ClearLeds();
            Program.SetAllLeds(color);
        }

        [Route("[controller]/Brightness")]
        [HttpPost]
        public void SetBrightness([FromBody] LedBrightness brightness)
        {
            Program.KillAllThreads();
            Program.SetBrightness(brightness.brightness);
        }

        [Route("[controller]/Sequence")]
        [HttpPost]
        public void SetSequence([FromBody] LedSequence ledSequence)
        {
            if (ledSequence.sequence == "Rainbow")      Program.Rainbow();
            if (ledSequence.sequence == "Carousel")     Program.Carousel();
            if (ledSequence.sequence == "Rgb")          Program.Rgb();
            if (ledSequence.sequence == "Jungle")       Program.Jungle();
            if (ledSequence.sequence == "Ocean")        Program.Ocean();
            if (ledSequence.sequence == "Oscillate")    Program.Oscillate();
        }

        [Route("[controller]/TurnOffLeds")]
        [HttpPost]
        public void TurnOff([FromBody] LedState ledState)
        {
            if (ledState.lightOn == false)
            {
                Program.KillAllThreads();
                Program.ClearLeds();
            }
        }
    }
}
