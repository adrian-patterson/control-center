using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace control
{
    public class LedState
    {
        public bool lightOn { get; set; }

        public LedState(bool lightOn)
        {
            this.lightOn = lightOn;
        }
    }
}
