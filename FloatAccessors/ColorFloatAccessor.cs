using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	public abstract class ColorFloatAccessor : Accessor<float>
	{
		public ColorChannel colorChannel;

		protected abstract Color GetColor();

		protected abstract void SetColor(Color c);


		public override float GetValue()
		{
			Color c = GetColor();
			switch (colorChannel)
			{
				case ColorChannel.A:
					return c.a;
				case ColorChannel.R:
					return c.r;
				case ColorChannel.G:
					return c.g;
				case ColorChannel.B:
					return c.b;
				default:
					throw new NotImplementedException("No case found for " + colorChannel);
			}
		}

		public override void SetValue(float f)
		{
			Color c = GetColor();
			switch (colorChannel)
			{
				case ColorChannel.A:
					c.a = f;
					break;
				case ColorChannel.R:
					c.r = f;
					break;
				case ColorChannel.G:
					c.g = f;
					break;
				case ColorChannel.B:
					c.b = f;
					break;
				default:
					throw new NotImplementedException("No case found for " + colorChannel);
			}
			SetColor(c);
		}


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
