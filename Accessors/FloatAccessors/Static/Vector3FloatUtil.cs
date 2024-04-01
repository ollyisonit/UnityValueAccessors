using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Gets and sets Vector3 axis values.
	/// </summary>
	public static class Vector3FloatUtil
	{
		/// <summary>
		/// Gets axis value from Vector3
		/// </summary>
		public static float GetValue(Axis3D axis, Vector3 vector)
		{
			switch (axis)
			{
				case Axis3D.X:
					return vector.x;
				case Axis3D.Y:
					return vector.y;
				case Axis3D.Z:
					return vector.z;
				default:
					throw new NotImplementedException(axis + " not implemented.");
			}
		}


		/// <summary>
		/// Sets axis value in Vector3 and returns the Vector3 with the axis set.
		/// </summary>
		public static Vector3 SetValue(Axis3D axis, Vector3 vector, float value)
		{
			switch (axis)
			{
				case Axis3D.X:
					vector.x = value;
					return vector;
				case Axis3D.Y:
					vector.y = value;
					return vector;
				case Axis3D.Z:
					vector.z = value;
					return vector;
				default:
					throw new NotImplementedException(axis + " not implemented.");
			}
		}
	}
}
