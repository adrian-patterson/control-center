using System;
using System.Collections.Generic;
using Raspberry.IO.GeneralPurpose;

namespace control
{
	public class Lighting
	{
		private static Dictionary<ushort, OutputPinConfiguration> pins;
		private GpioConnection connection;

		public bool lightStatus { get; set; }

		public Lighting()
		{
			//pins = new Dictionary<ushort, OutputPinConfiguration> {
			//	{ 23, ProcessorPin.Pin23.Output() },
			//	{ 24, ProcessorPin.Pin24.Output() }
			//};

			connection = new GpioConnection(ProcessorPin.Pin23.Output());
		}

		public void toggleLight()
		{
			connection.Blink(ProcessorPin.Pin23, TimeSpan.FromSeconds(1));
			Console.WriteLine("Pin should be toggled.");
		}
	}
}
