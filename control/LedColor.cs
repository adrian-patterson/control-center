using System;
using System.Drawing;
using System.Threading;
using rpi_ws281x;

namespace control
{
    public class LedColor
    {
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }
        public int brightness { get; set; }
        public bool lightOn { get; set; }
        public static readonly int ledCount = 88;

        public LedColor(int r, int g, int b, int brightness, bool lightOn)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.brightness = brightness;
            this.lightOn = lightOn;
        }

        // Set strip to RGB data members
        public void SetRgb()
        {
            Console.WriteLine("Set RGB Function Called");
            var settings = LedInit();
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
            Thread.Sleep(100);
        }
        
        // LED Initialization
        public rpi_ws281x.Settings LedInit()
        {
            var settings = Settings.CreateDefaultSettings(false);
            var controller = settings.AddController(ledCount, Pin.Gpio18, StripType.WS2812_STRIP, 255, false);

            using (var rpi = new WS281x(settings))
            {
                rpi.Reset();
                rpi.Dispose();
            }
            return settings;
        }
    }
}
