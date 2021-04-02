using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using rpi_ws281x;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace control
{
    public class Program
    {
        public static WS281x rpi;
        public static readonly int ledCount = 88;
        public static int ledBrightness = 255;
        public static Thread rainbow, carousel, rgb, jungle;
        public static List<Thread> threads = new();
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
            ledBrightness = brightness;
            rpi.SetBrightness(ledBrightness);
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

        public static void Jungle()
        {
            KillAllThreads();

            jungle = NewJungleThread();
            jungle.Start();
        }

        public static void KillAllThreads()
        {
            if (carousel.IsAlive) carousel.Interrupt();
            if (rainbow.IsAlive) rainbow.Interrupt();
            if (rgb.IsAlive) rgb.Interrupt();
            if (jungle.IsAlive) jungle.Interrupt();
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
                        Thread.Sleep(200);
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

        public static Thread NewJungleThread()
        {
            return new Thread(() =>
            {
                int r = 0;
                int g = 255;
                int b = 15;

                try
                {
                    while (true)
                    {
                        Color shadeOfGreen = new Color();
                        while (g <= 255 && g > 100)
                        {
                            Console.WriteLine("Jungle Sequence");
                            shadeOfGreen = Color.FromArgb(r, b, g);
                            SetAllLeds(shadeOfGreen);

                            g--;
                            Thread.Sleep(100);
                        }
                        while (g < 255)
                        {
                            Console.WriteLine("Jungle Sequence");
                            shadeOfGreen = Color.FromArgb(r, b, g);
                            SetAllLeds(shadeOfGreen);

                            g++;
                            Thread.Sleep(100);
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
                Color.LightGoldenrodYellow,
                Color.Black
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
        }
    }
}