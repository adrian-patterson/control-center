using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using rpi_ws281x;

namespace control
{
    public class LedSequence
    {
        public string sequence { get; set; }
        public static readonly int ledCount = 88;

        public LedSequence(string sequence)
        {
            this.sequence = sequence;
        }

        public void ExecuteSequence()
        {
            MethodInfo seqInfo = this.GetType().GetMethod(this.sequence);
            seqInfo.Invoke(this, null);
        }
        // Rainbow function. First initializes LED strip, then loops while performing action.
        public void Rainbow()
        {
            Console.WriteLine("Rainbow Sequence Called");

            Task.Run(() =>
            {
                var settings = LedInit();
                using (var rpi = new WS281x(settings))
                {
                    int colorOffset = 0;

                    while (true)
                    {
                        var colors = GetRainbowColors();
                        for (var i = 0; i < ledCount; i++)
                        {
                            var colorIndex = (i + colorOffset) % colors.Count;
                            rpi.SetLed(i, colors[colorIndex]);
                        }
                        rpi.Render();
                        colorOffset = (colorOffset + 1) % colors.Count;
                        Task.Delay(500);
                    }
                }
            }).Wait();
        }

        public void Carousel()
        {
            Console.WriteLine("Carousel Sequence Called");

            Task.Run(() =>
            {
                var settings = LedInit();
                using var rpi = new WS281x(settings);
                int colorOffset = 0;

                while (true)
                {
                    var colors = GetRainbowColors();
                    for (var i = 0; i < ledCount; i++)
                    {
                        var colorIndex = (i + colorOffset) % colors.Count;
                        rpi.SetLed(i, colors[colorIndex]);
                    }
                    rpi.Render();
                    colorOffset = (colorOffset + 1) % colors.Count;
                    Task.Delay(500);
                }
            }).Wait();
        }

        public void RGB()
        {
            Console.WriteLine("ColorWipe Sequence Called");

            Task.Run(() =>
            {
                var settings = LedInit();
                using var rpi = new WS281x(settings);

                while (true)
                {
                    foreach (var color in GetRgbColors())
                    {
                        for (var i = 0; i < ledCount; i++)
                        {
                            rpi.SetLed(i, color);
                            rpi.Render();
                            Task.Delay(500);
                        }
                    }
                }
            }).Wait();
        }

        public void Jungle()
        {
            Console.WriteLine("Jungle Sequence Called");

            Task.Run(() =>
            {
                var settings = LedInit();
                using var rpi = new WS281x(settings);

                int r = 0;
                int g = 255;
                int b = 15;

                while (true)
                {
                    Color shadeOfGreen = new Color();
                    shadeOfGreen = Color.FromArgb(r, b, g);

                    rpi.SetAll(shadeOfGreen);
                    rpi.Render();

                    Task.Delay(10);
                }
            }).Wait();
        }
    

        public Settings LedInit()
        {
            var settings = Settings.CreateDefaultSettings(false);
            settings.AddController(ledCount, Pin.Gpio18, StripType.WS2812_STRIP, 255, false);

            using (var rpi = new WS281x(settings))
            {
                rpi.Reset();
            }

            Thread.Sleep(100);

            return settings;
        }

        public static List<Color> GetRainbowColors()
        {
            var result = new List<Color>
            {
                Color.Red,
                Color.DarkOrange,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Purple,
                Color.DeepPink
            };

            return result;
        }

        public static List<Color> GetCarouselColors()
        {
            var result = new List<Color>
            {
                Color.White,
                Color.Black
            };

            return result;
        }

        public static List<Color> GetRgbColors()
        {
            var result = new List<Color>
            {
                Color.Red,
                Color.Green,
                Color.Blue
            };

            return result;
        }
    }
}
