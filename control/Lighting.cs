using System;
using System.Drawing;
using System.Threading;
using rpi_ws281x;

namespace control
{
    public class Lighting
    {
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }

        public Lighting(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public void setRgb()
        {
            Console.WriteLine("R: " + r + "\tG: " + g + "\tB: " + b);

            var settings = Settings.CreateDefaultSettings(false);
            var controller = settings.AddController(88, Pin.Gpio18, StripType.WS2812_STRIP, 255, false);

            Color color = new Color();
            color = Color.FromArgb(r, b, g);

            using (var rpi = new WS281x(settings))
            {
                var ledCount = rpi.GetLedCount();
                var ledBrightness = rpi.GetBrightness();
                Console.WriteLine("Brightness:\t" + ledBrightness);
                Console.WriteLine(ledCount + "LEDs Detected\n\n");
                rpi.SetLedCount(ledCount);
                rpi.SetAll(color);
            }
            Thread.Sleep(10);
        }
	}
}
