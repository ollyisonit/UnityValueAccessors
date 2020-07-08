﻿using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class FloatVector2Accessor : Accessor<Vector2>
	{
		public enum AccessMode
		{
			Split,
			Merged
		}

		public AccessMode accessMode;

		[ConditionalHide("accessMode", AccessMode.Merged)]
		public AnyFlatFloatAccessor Float;

		[ConditionalHide("accessMode", AccessMode.Merged)]
		public Axis2D sourceAxis;

		[ConditionalHide("accessMode", AccessMode.Split)]
		public AnyFlatFloatAccessor x;

		[ConditionalHide("accessMode", AccessMode.Split)]
		public AnyFlatFloatAccessor y;


		public override Vector2 GetValue()
		{
			switch (accessMode)
			{
				case AccessMode.Split:
					return new Vector2(x.Value, y.Value);
				case AccessMode.Merged:
					float v = Float.Value;
					return new Vector2(v, v);
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
		}

		public override void SetValue(Vector2 value)
		{
			switch (accessMode)
			{
				case AccessMode.Split:
					x.Value = value.x;
					y.Value = value.y;
					break;
				case AccessMode.Merged:
					Float.Value = Vector2FloatAccessor.GetValue(sourceAxis, value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessMode);
			}
		}
	}

}