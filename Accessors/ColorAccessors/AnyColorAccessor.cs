using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Access a color using any existing color accessor.
	/// </summary>
	[Serializable]
	public class AnyColorAccessor : Accessor<Color>
	{
		public enum AccessType
		{
			Image = 0,
			Light = 1,
			Reflected = 2,
			Custom = 3,
			Constant = 4
		}

		[Tooltip("Where should the value be accessed from?")]
		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Image, "Accessor")]
		public ImageColorAccessor image;

		[ConditionalHide("accessType", AccessType.Light, "Accessor")]
		public LightColorAccessor light;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomColorAccessor custom;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedColorAccessor reflectedAccess;

		[ConditionalHide("accessType", AccessType.Constant, "Accessor")]
		public ConstantColorAccessor constant;

		protected override Color GetValue()
		{
			switch (accessType)
			{
				case AccessType.Image:
					return image.Value;
				case AccessType.Light:
					return light.Value;
				case AccessType.Custom:
					return custom.Value;
				case AccessType.Constant:
					return constant.Value;
				case AccessType.Reflected:
					return reflectedAccess.Value;
				default:
					throw new NotImplementedException("Case not found for AccessType " + accessType);
			}
		}

		protected override void SetValue(Color value)
		{
			switch (accessType)
			{
				case AccessType.Image:
					image.Value = value;
					break;
				case AccessType.Light:
					light.Value = value;
					break;
				case AccessType.Custom:
					custom.Value = value;
					break;
				case AccessType.Constant:
					constant.Value = value;
					break;
				case AccessType.Reflected:
					reflectedAccess.Value = value;
					break;
				default:
					throw new NotImplementedException("Case not found for AccessType " + accessType);
			}
		}

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			custom = attachedObject.GetComponent<CustomColorAccessor>();
		}
	}
}