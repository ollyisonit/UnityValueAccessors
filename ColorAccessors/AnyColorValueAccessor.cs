using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public class AnyColorValueAccessor : ValueAccessor<Color>
	{
		public enum AccessType
		{
			Image,
			Light,
			Custom,
			Reflected
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Image, "Accessor")]
		public ImageColorValueAccessor image;

		[ConditionalHide("accessType", AccessType.Light, "Accessor")]
		public LightColorValueAccessor light;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomColorValueAccessor custom;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedColorValueAccessor reflected;

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
				case AccessType.Reflected:
					return reflected.GetValue();
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
				case AccessType.Reflected:
					 reflected.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for AccessType " + accessType);
			}
		}

		public override void Reset(GameObject attachedObject)
		{
			image = new ImageColorValueAccessor();
			image.Reset(attachedObject);
			light = new LightColorValueAccessor();
			light.Reset(attachedObject);
			custom = attachedObject.GetComponent<CustomColorValueAccessor>();
			reflected = new ReflectedColorValueAccessor();
			reflected.Reset(attachedObject);
		}
	}
}