using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
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

		public override Vector3 GetValue()
		{
			switch (accessType)
			{
				case AccessType.Transform:
					return trans.GetValue();
				case AccessType.Custom:
					return custom.GetValue();
				#region NESTED
				case AccessType.Vector2:
					return vector2.GetValue();
				case AccessType.Float:
					return Float.Value;
				#endregion
				case AccessType.Constant:
					return constant.GetValue();
				case AccessType.Reflected:
					return reflectedAccess.GetValue();
				case AccessType.Random:
					return random.GetValue();
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}


		public override void SetValue(Vector3 value)
		{
			switch (accessType)
			{
				case AccessType.Transform:
					trans.SetValue(value);
					break;
				case AccessType.Custom:
					custom.SetValue(value);
					break;
				#region NESTED
				case AccessType.Vector2:
					vector2.SetValue(value);
					break;
				case AccessType.Float:
					Float.Value = value;
					break;
				#endregion
				case AccessType.Constant:
					constant.SetValue(value);
					break;
				case AccessType.Reflected:
					reflectedAccess.SetValue(value);
					break;
				case AccessType.Random:
					random.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}