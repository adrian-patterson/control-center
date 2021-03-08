using System;
using System.Device.Gpio;

namespace control
{
    public class Lighting
    {
        GpioController controller = new GpioController();
        private static readonly int pin = 23;
        public string toggle { get; set; }
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }

        public Lighting()
		{
            controller.OpenPin(pin, PinMode.Output);
        }

		public void ledOn()
        {
            controller.Write(pin, PinValue.High);
            Console.WriteLine("LED on");
		}

		public void ledOff()
        {
            controller.Write(pin, PinValue.Low);
            Console.WriteLine("LED off");
		}

        public void setRgb()
        {
            // ws281.setColor(r,g,b)
            // you're garb
            // you're going to have to use Color objects to set them
            // Color myColor = new Color(r,g,b)
            // also will have to import color library 'using System.Color'
        }
	}
}
