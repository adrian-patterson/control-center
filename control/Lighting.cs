using System;
using System.Device.Gpio;

namespace control
{
	public class Lighting
	{
        GpioController controller = new GpioController();
        private static readonly int pin = 23;
		public string toggle { get; set; }

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
	}
}
