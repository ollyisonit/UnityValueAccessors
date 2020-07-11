using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Wrapper for all Vector3Accessors that don't contain nested references to AnyVector3Accessor
	/// </summary>
	[Serializable]
	public class AnyFlatVector3Accessor : Accessor<Vector3>
	{
		public enum AccessType
		{
			Transform = 0,
			Reflected = 3,
			Custom = 4,
			Constant = 5,
			Random = 6

		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Transform, "Accessor")]
		public TransformVector3Accessor trans;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector3Accessor custom;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector3Accessor reflectedAccess;


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