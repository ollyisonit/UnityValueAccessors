using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses a Vector3 by converting a float value.
	/// </summary>
	[Serializable]
	public class FloatVector3Accessor : Accessor<Vector3>
	{

		public enum AccessMode
		{
			Split = 0,
			Merged = 1
		}

		[Tooltip("Should each axis of the Vector3 be considered individually or should they all share the same value?")]
		public AccessMode accessMode;

		[ConditionalHide("accessMode", AccessMode.Merged)]
		public AnyFlatFloatAccessor Float;

		[ConditionalHide("accessMode", AccessMode.Merged)]
		public Axis3D sourceAxis;

		[ConditionalHide("accessMode", AccessMode.Split)]
		public AnyFlatFloatAccessor x;

		[ConditionalHide("accessMode", AccessMode.Split)]
		public AnyFlatFloatAccessor y;

		[ConditionalHide("accessMode", AccessMode.Split)]
		public AnyFlatFloatAccessor z;


		protected override Vector3 GetValue()
		{
			switch (accessMode)
			{
				case AccessMode.Split:
					return new Vector3(x.Value, y.Value, z.Value);
				case AccessMode.Merged:
					float v = Float.Value;
					return new Vector3(v, v, v);
				default:
					throw new NotImplementedException("Case not found for " + accessMode);
			}
		}


		protected override void SetValue(Vector3 value)
		{
			switch (accessMode)
			{
				case AccessMode.Split:
					x.Value = value.x;
					y.Value = value.y;
					z.Value = value.z;
					break;
				case AccessMode.Merged:
					Float.Value = Vector3FloatUtil.GetValue(sourceAxis, value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessMode);
			}
		}
	}
}