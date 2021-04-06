using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using rpi_ws281x;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace control
{
    public class Program
    {
        public static WS281x rpi;
        public static readonly int ledCount = 88;
        public static Thread rainbow, carousel, rgb, jungle, ocean, oscillate;
        public static List<Color> rainbowColors, carouselColors, rgbColors;

        public static void Main(string[] args)
        {
            SequenceThreadInit();
            LedInit();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void LedInit()
        {
            var settings = Settings.CreateDefaultSettings(false);
            settings.AddController(ledCount, Pin.Gpio18, StripType.WS2812_STRIP, 255, false);

            rpi = new WS281x(settings);
            rpi.SetLedCount(ledCount);
            rpi.Reset();
        }

        public static void SetAllLeds(Color color)
        {
            rpi.SetAll(color);
            rpi.Render();
        }

        public static void SetSpecificLed(int number, Color color)
        {
            rpi.SetLed(number, color);
            rpi.Render();
        }

        public static void SetBrightness(int brightness)
        {
            KillAllThreads();
            
            rpi.SetBrightness(brightness);
            rpi.Render();
        }

        public static void ClearLeds()
        {
            rpi.Reset();
        }

        public static void Rainbow()
        {
            KillAllThreads();

            rainbow = NewRainbowThread();
            rainbow.Start();
        }
        public static void Carousel()
        {
            KillAllThreads();

            carousel = NewCarouselThread();
            carousel.Start();
        }

        public static void Rgb()
        {
            KillAllThreads();

            rgb = NewRgbThread();
            rgb.Start();
        }


        public static void Oscillate()
        {
            KillAllThreads();

            oscillate = NewOscillateThread();
            oscillate.Start();
        }

        public static void Jungle()
        {
            KillAllThreads();

            jungle = NewJungleThread();
            jungle.Start();
        }

        public static void Ocean()
        {
            KillAllThreads();

            ocean = NewOceanThread();
            ocean.Start();
        }

        public static void KillAllThreads()
        {
            if (carousel.IsAlive)   carousel.Interrupt();
            if (rainbow.IsAlive)    rainbow.Interrupt();
            if (rgb.IsAlive)        rgb.Interrupt();
            if (jungle.IsAlive)     jungle.Interrupt();
            if (ocean.IsAlive)      ocean.Interrupt();
            if (oscillate.IsAlive)  oscillate.Interrupt();
        }

        public static Thread NewRainbowThread()
        {
            int rainbowColorOffset = 0;
            return new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine("Rainbow Sequence");
                        for (var i = 0; i < ledCount; i++)
                        {
                            var colorIndex = (i + rainbowColorOffset) % rainbowColors.Count;
                            SetSpecificLed(i, rainbowColors[colorIndex]);
                        }
                        rainbowColorOffset = (rainbowColorOffset + 1) % rainbowColors.Count;
                        Thread.Sleep(200);
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            });
        }

        public static Thread NewCarouselThread()
        {
            int carouselColorOffset = 0;
            
            return new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine("Carousel Sequence");
                        for (var i = 0; i < ledCount; i++)
                        {
                            var colorIndex = (i + carouselColorOffset) % carouselColors.Count;
                            SetSpecificLed(i, carouselColors[colorIndex]);
                        }
                        carouselColorOffset = (carouselColorOffset + 1) % carouselColors.Count;
                        Thread.Sleep(100);
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            });
        }

        public static Thread NewRgbThread()
        {
            return new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine("Rgb Sequence");
                        foreach (var color in rgbColors)
                        {
                            SetAllLeds(color);   
                            Thread.Sleep(1000);
                        }
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            });
        }

        public static Thread NewOscillateThread()
        {
            return new Thread(() =>
            {
                try
                {
                    float progress = 0;
                    while (true)
                    {
                        var color = OscillateColors(progress);
                        progress += 0.01f;

                        SetAllLeds(color);
                        Thread.Sleep(100);
                        if (progress >= 1.0f)
                            progress = 0;
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            });
        }

        public static Thread NewJungleThread()
        {
            return new Thread(() =>
            {
                int r = 15;
                int g = 255;
                int b = 15;

                try
                {
                    while (true)
                    {
                        Color shadeOfGreen = new Color();
                        while (g <= 255 && g > 50)
                        {
                            shadeOfGreen = Color.FromArgb(r, b, g);
                            Console.WriteLine("Jungle Sequence: Color at " + shadeOfGreen);
                            SetAllLeds(shadeOfGreen);

                            g--;
                            Thread.Sleep(10);
                        }
                        while (g < 255)
                        {
                            shadeOfGreen = Color.FromArgb(r, b, g);
                            Console.WriteLine("Jungle Sequence: Color at " + shadeOfGreen);
                            SetAllLeds(shadeOfGreen);

                            g++;
                            Thread.Sleep(10);
                        }
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            });
        }

        public static Thread NewOceanThread()
        {
            return new Thread(() =>
            {
                int r = 15;
                int g = 15;
                int b = 255;

                try
                {
                    while (true)
                    {
                        Color shadeOfGreen = new Color();
                        while (b <= 255 && b > 50)
                        {
                            shadeOfGreen = Color.FromArgb(r, b, g);
                            Console.WriteLine("Ocean Sequence: Color at " + shadeOfGreen);
                            SetAllLeds(shadeOfGreen);

                            b--;
                            Thread.Sleep(10);
                        }
                        while (b < 255)
                        {
                            shadeOfGreen = Color.FromArgb(r, b, g);
                            Console.WriteLine("Ocean Sequence: Color at " + shadeOfGreen);
                            SetAllLeds(shadeOfGreen);

                            b++;
                            Thread.Sleep(10);
                        }
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            });
        }

        public static void SequenceThreadInit()
        {
            rgbColors = new List<Color>
            {
                Color.Red,
                Color.Green,
                Color.Blue
            };

            carouselColors = new List<Color>
            {
                Color.Yellow,
                Color.White
            };

            rainbowColors = new List<Color>
            {
                Color.Red,
                Color.DarkOrange,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Purple,
                Color.DeepPink
            };

            rainbow = NewRainbowThread();
            carousel = NewCarouselThread();
            rgb = NewRgbThread();
            jungle = NewJungleThread();
            oscillate = NewOscillateThread();
            ocean = NewOceanThread();
        }

        public static Color OscillateColors(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }
    }
}