using System;
using System.Device.Gpio;
using (var ws281 = new WS281x(settings));
using System.Color;

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
            settings.Channels[0] = new Channel(16, 18, 255, false, StripType.WS2812_STRIP);
            for (int a = 0; a < 88; a = a + 1)
            {
                ws281.SetLEDColor(0, a, Color.Red);
                ws281.Render();
            }
            //Adrian's comments:
            // ws281.setColor(r,g,b)
            // you're going to have to use Color objects to set them
            // Color myColor = new Color(r,g,b)
            // also will have to import color library 'using System.Color'

            //Julian's comments:
            //This should set the whole strip to the red color
            // I need to further explore the library to find how to set LED colors
            // using RGB values instead of color.red
            // might not work (unknown errors) 
        }
	}
}
