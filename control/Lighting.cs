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
        public bool lightOn { get; set; }
        public static readonly int ledCount = 88;

        public Lighting(int r, int g, int b, bool lightOn)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.lightOn = lightOn;
        }

        public void setRgb()
        {
            var settings = Settings.CreateDefaultSettings(false);
            var controller = settings.AddController(ledCount, Pin.Gpio18, StripType.WS2812_STRIP, 255, false);

            using (var rpi = new WS281x(settings))
            {
                if (lightOn == true)
                {
                    Console.WriteLine("R: " + r + "\tG: " + g + "\tB: " + b);

                    Color color = new Color();
                    color = Color.FromArgb(r, b, g);

                    var ledBrightness = rpi.GetBrightness();
                    Console.WriteLine("Brightness:\t" + ledBrightness);
                    rpi.SetLedCount(ledCount);
                    rpi.SetAll(color);
                    Console.WriteLine("LED Turned on.");
                }
                else
                {
                    Console.WriteLine("LED Turned off.");
                    rpi.Reset();
                    rpi.Dispose();
                }
            }
            Thread.Sleep(100);
        }
    }
}
