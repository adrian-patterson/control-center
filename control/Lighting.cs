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
        public int brightness { get; set; }
        public bool lightOn { get; set; }
        public static readonly int ledCount = 88;

        public Lighting(int r, int g, int b, int brightness, bool lightOn)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.brightness = brightness;
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
                    Console.WriteLine("Brightness: " + brightness);

                    Color color = new Color();
                    color = Color.FromArgb(r, b, g);

                    rpi.SetBrightness(brightness);
                    rpi.SetLedCount(ledCount);
                    rpi.SetAll(color);
                }
                else
                {
                    Console.WriteLine("LED Turned off.");
                    rpi.Reset();
                    rpi.Dispose();
                }
            }
            Thread.Sleep(10);
        }
    }
}
