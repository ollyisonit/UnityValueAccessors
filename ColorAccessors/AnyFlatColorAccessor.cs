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
			Custom,
			Constant
		}

		public bool reflected;

		[ConditionalHide("reflected", false)]
		public AccessType accessType;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Image }, "Accessor")]
		public ImageColorAccessor image;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Light }, "Accessor")]
		public LightColorAccessor light;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Custom }, "Accessor")]
		public CustomColorAccessor custom;

		[ConditionalHide("reflected", true, "Accessor")]
		public ReflectedColorAccessor reflectedAccess;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Constant }, "Accessor")]
		public ConstantColorAccessor constant;

		public override Color GetValue()
		{
			if (reflected)
			{
				return reflectedAccess.Value;
			}
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
				default:
					throw new NotImplementedException("Case not found for AccessType " + accessType);
			}
		}

		public override void SetValue(Color value)
		{
			if (reflected)
			{
				reflectedAccess.Value = value;
				return;
			}

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