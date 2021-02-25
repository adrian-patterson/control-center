using System;
using System.Collections.Generic;
using System.Device.Gpio;

namespace control
{
	public class Lighting
	{
		GpioController controller = new GpioController();
		private static readonly int pin = 23;

		public bool lightStatus { get; set; }

		public Lighting()
		{
			//pins = new Dictionary<ushort, OutputPinConfiguration> {
			//	{ 23, ProcessorPin.Pin23.Output() },
			//	{ 24, ProcessorPin.Pin24.Output() }
			//};

			controller.OpenPin(pin, PinMode.Output);
		}

		public void toggleLight()
		{
			if(lightStatus)
            {
				controller.Write(pin, PinValue.Low);
            }
			else
            {
				controller.Write(pin, PinValue.High);
            }
			Console.WriteLine("Pin should be toggled.");
			lightStatus = !lightStatus;
		}
	}
}
