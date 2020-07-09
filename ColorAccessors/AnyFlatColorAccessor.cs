using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class AnyFlatColorAccessor : Accessor<Color>
	{
		public enum AccessType
		{
			Image,
			Light,
			Reflected,
			Custom,
			Constant
		}

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

		public override Color GetValue()
		{
			switch (accessType)
			{
				case AccessType.Image:
					return image.GetValue();
				case AccessType.Light:
					return light.GetValue();
				case AccessType.Custom:
					return custom.GetValue();
				case AccessType.Constant:
					return constant.GetValue();
				case AccessType.Reflected:
					return reflectedAccess.GetValue();
				default:
					throw new NotImplementedException("Case not found for AccessType " + accessType);
			}
		}

		public override void SetValue(Color value)
		{
			switch (accessType)
			{
				case AccessType.Image:
					image.SetValue(value);
					break;
				case AccessType.Light:
					light.SetValue(value);
					break;
				case AccessType.Custom:
					custom.SetValue(value);
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

		public override void Reset(GameObject attachedObject)
		{
			image = new ImageColorAccessor();
			image.Reset(attachedObject);
			light = new LightColorAccessor();
			light.Reset(attachedObject);
			custom = attachedObject.GetComponent<CustomColorAccessor>();
			reflectedAccess = new ReflectedColorAccessor();
			reflectedAccess.Reset(attachedObject);
			constant = new ConstantColorAccessor();
			constant.Reset(attachedObject);
		}
	}
}