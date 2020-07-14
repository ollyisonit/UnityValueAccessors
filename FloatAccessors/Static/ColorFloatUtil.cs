using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Gets and sets specific color channels using ColorChannel enum.
	/// </summary>
	public static class ColorFloatUtil
	{
		/// <summary>
		/// Gets value of color's channel.
		/// </summary>
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


		/// <summary>
		/// Sets value of color's channel.
		/// </summary>
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
