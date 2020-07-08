using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	public static class Vector3FloatAccessor
	{
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
