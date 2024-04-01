﻿using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Accesses a Vector3 using any of the other defined Vector3Accessors.
	/// </summary>
	[Serializable]
	public class AnyVector3Accessor : Accessor<Vector3>
	{
		public enum AccessType
		{
			Transform = 0,
			#region NESTED
			Float = 1,
			Vector2 = 2,
			#endregion
			Reflected = 3,
			Custom = 4,
			Constant = 5,
			Random = 6

		}

		[Tooltip("Where should the value be accessed from?")]
		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Transform, "Accessor")]
		public TransformVector3Accessor trans;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector3Accessor custom;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector3Accessor reflectedAccess;

		#region NESTED
		[ConditionalHide("accessType", AccessType.Vector2, "Accessor")]
		public Vector2Vector3Accessor vector2;

		[ConditionalHide("accessType", AccessType.Float, "Accessor")]
		public FloatVector3Accessor Float;
		#endregion

		[ConditionalHide("accessType", AccessType.Constant, "Accessor")]
		public ConstantVector3Accessor constant;

		[ConditionalHide("accessType", AccessType.Random, "Accessor")]
		public RandomVector3Accessor random;

		protected override Vector3 GetValue()
		{
			switch (accessType)
			{
				case AccessType.Transform:
					return trans.Value;
				case AccessType.Custom:
					return custom.Value;
				#region NESTED
				case AccessType.Vector2:
					return vector2.Value;
				case AccessType.Float:
					return Float.Value;
				#endregion
				case AccessType.Constant:
					return constant.Value;
				case AccessType.Reflected:
					return reflectedAccess.Value;
				case AccessType.Random:
					return random.Value;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}


		protected override void SetValue(Vector3 value)
		{
			switch (accessType)
			{
				case AccessType.Transform:
					trans.Value = value;
					break;
				case AccessType.Custom:
					custom.Value = value;
					break;
				#region NESTED
				case AccessType.Vector2:
					vector2.Value = value;
					break;
				case AccessType.Float:
					Float.Value = value;
					break;
				#endregion
				case AccessType.Constant:
					constant.Value = value;
					break;
				case AccessType.Reflected:
					reflectedAccess.Value = value;
					break;
				case AccessType.Random:
					random.Value = value;
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}