using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class FloatVector3Accessor : Accessor<Vector3>
	{

		public enum AccessMode
		{
			Split = 0,
			Merged = 1
		}

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


		public override Vector3 GetValue()
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

		public override void Reset(GameObject o)
		{
			Float = new AnyFlatFloatAccessor();
			Float.Reset(o);
			x = new AnyFlatFloatAccessor();
			x.Reset(o);
			y = new AnyFlatFloatAccessor();
			y.Reset(o);
			z = new AnyFlatFloatAccessor();
			z.Reset(o);
		}

		public override void SetValue(Vector3 value)
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