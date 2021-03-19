using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
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

            //var settings = LedInit();
            //using var rpi = new WS281x(settings);

            //int colorOffset = 0;

            //while (true)
            //{
            //    var colors = GetRainbowColors();
            //    for (var i = 0; i < ledCount; i++)
            //    {
            //        var colorIndex = (i + colorOffset) % colors.Count;
            //        rpi.SetLed(i, colors[colorIndex]);
            //    }
            //    rpi.Render();
            //    colorOffset = (colorOffset + 1) % colors.Count;

            //    Thread.Sleep(500);
            //}
        }
        public rpi_ws281x.Settings LedInit()
        {
            var settings = Settings.CreateDefaultSettings(false);
            settings.AddController(ledCount, Pin.Gpio18, StripType.WS2812_STRIP, 255, false);

            return settings;
        }

        public static List<Color> GetRainbowColors()
        {
            var result = new List<Color>();

            result.Add(Color.Red);
            result.Add(Color.DarkOrange);
            result.Add(Color.Yellow);
            result.Add(Color.Green);
            result.Add(Color.Blue);
            result.Add(Color.Purple);
            result.Add(Color.DeepPink);

            return result;
        }
    }
}
