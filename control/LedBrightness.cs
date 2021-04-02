using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace control
{
    public class LedBrightness
    {
        public int brightness { get; set; }

        public LedBrightness(int brightness)
        {
            this.brightness = brightness;
        }
    }
}
