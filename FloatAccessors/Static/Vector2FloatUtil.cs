using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Gets and sets axes of Vector2s
	/// </summary>
	public static class Vector2FloatUtil
	{
		/// <summary>
		/// Gets axis value from Vector2
		/// </summary>
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


		/// <summary>
		/// Sets axis value in Vector2 and returns the Vector2 with value set.
		/// </summary>
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
