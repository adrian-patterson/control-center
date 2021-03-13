using System;
using System.Drawing;
using rpi_ws281x;

namespace control
{
    public class Lighting
    {
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }

        public void setRgb()
        {
            Console.WriteLine("R: " + r + "\tG: " + g + "\tB: " + b);

            var settings = Settings.CreateDefaultSettings(false);
            var controller = settings.AddController(88, Pin.Gpio18, StripType.WS2812_STRIP, 255, false);

            Color color = new Color();
            color = Color.FromArgb(r, g, b);

            using (var rpi = new WS281x(settings))
            {
                rpi.SetLedCount(88);
                rpi.SetAll(color);
            }
        }
	}
}
