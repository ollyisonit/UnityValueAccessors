using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	public static class Vector2FloatAccessor
	{
		public static float GetValue(Axis2D axis, Vector2 v)
		{
			switch (axis)
			{
				case Axis2D.X:
					return v.x;
				case Axis2D.Y:
					return v.y;
				default:
					throw new NotImplementedException("No case found for axis " + axis);
			}
		}


		public static Vector2 SetValue(Axis2D axis, Vector2 v, float value)
		{
			switch (axis)
			{
				case Axis2D.X:
					v.x = value;
					break;
				case Axis2D.Y:
					v.y = value;
					break;
				default:
					throw new NotImplementedException("No case found for axis " + axis);
			}
			return v;
		}
	}
}
