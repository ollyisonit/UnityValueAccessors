using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Wrapper for all Vector2Accessors that don't include nested references to AnyVector2Accessor.
	/// </summary>
	[Serializable]
	public class AnyFlatVector2Accessor : Accessor<Vector2>
	{
		public enum AccessType
		{
			RectTransform = 0,
			Reflected = 3,
			Custom = 4,
			Constant = 5,
			Random = 6
		}

		[Tooltip("Where should the value be accessed from?")]
		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.RectTransform, "Accessor")]
		public RectTransformVector2Accessor rect;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector2Accessor cust;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector2Accessor reflect;

		[ConditionalHide("accessType", AccessType.Constant, "Accessor")]
		public ConstantVector2Accessor constant;

		[ConditionalHide("accessType", AccessType.Random, "Accessor")]
		public RandomVector2Accessor random;

		protected override Vector2 GetValue()
		{
			switch (accessType)
			{
				case AccessType.RectTransform:
					return rect.Value;
				case AccessType.Custom:
					return cust.Value;
				case AccessType.Constant:
					return constant.Value;
				case AccessType.Reflected:
					return reflect.Value;
				case AccessType.Random:
					return random.Value;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}


		protected override void SetValue(Vector2 value)
		{
			switch (accessType)
			{
				case AccessType.RectTransform:
					rect.Value = value;
					break;
				case AccessType.Custom:
					cust.Value = value;
					break;
				case AccessType.Constant:
					constant.Value = value;
					break;
				case AccessType.Reflected:
					reflect.Value = value;
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