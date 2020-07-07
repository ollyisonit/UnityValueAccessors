using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	public class ColorFloatAccessor
	{
		public static float GetChannel(Color c, ColorChannel channel)
		{
			switch (channel)
			{
				case (ColorChannel.R):
					return c.r;
				case (ColorChannel.G):
					return c.g;
				case (ColorChannel.B):
					return c.b;
				case (ColorChannel.A):
					return c.a;
				default:
					throw new NotImplementedException("Case not found for ColorChannel " + channel);
			}
		}


		public static Color SetChannel(Color c, ColorChannel channel, float value)
		{
			switch (channel)
			{
				case (ColorChannel.R):
					c.r = value;
					break;
				case (ColorChannel.G):
					c.g = value;
					break;
				case (ColorChannel.B):
					c.b = value;
					break;
				case (ColorChannel.A):
					c.a = value;
					break;
				default:
					throw new NotImplementedException("Case not found for ColorChannel " + channel);
			}
			return c;
		}
	}
}
