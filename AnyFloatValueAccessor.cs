using dninosores.UnityConditionalHideAttribute;
using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	/// <summary>
	/// Value Accessor that merges functionality of all standard float accessors into a single class for serialization in the unity editor.
	/// </summary>
	[Serializable]
	public class AnyFloatValueAccessor : ValueAccessor<float>
	{
		public enum AccessType
		{
			Transform,
			RectTransform,
			Light,
			ImageColor,
			Custom,
			Reflected
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Transform)]
		public TransformFloatValueAccessor transformToModify;

		#region LightModifiers
		public enum LightAttribute
		{
			General,
			Color
		}

		[ConditionalHide(new string[] { "accessType" }, new object[] { AccessType.Light })]
		public LightAttribute lightAttribute;

		[ConditionalHide(new string[] { "accessType", "lightAttribute" }, new object[] { AccessType.Light, LightAttribute.General })]
		public LightFloatValueAccessor lightToModify;

		[ConditionalHide(new string[] { "accessType", "lightAttribute" }, new object[] { AccessType.Light, LightAttribute.Color })]
		public LightColorFloatValueAccessor lightColorToModify;

		#endregion

		[ConditionalHide("accessType", AccessType.RectTransform)]
		public RectTransformFloatValueAccessor rectToModify;

		[ConditionalHide("accessType", AccessType.Custom), Tooltip("Make a script that extends CustomFloatValueAccessor and reference it here")]
		public CustomFloatValueAccessor customAccessor;

		[ConditionalHide("accessType", AccessType.ImageColor)]
		public ImageColorFloatValueAccessor imageToModify;

		#region ReflectedModifiers

		public enum ReflectionType
		{
			Simple,
			Nested
		}

		[ConditionalHide("accessType", AccessType.Reflected)]
		public ReflectionType reflectionType;

		[ConditionalHide(new string[] { "accessType", "reflectionType" }, new object[] { AccessType.Reflected, ReflectionType.Simple })]
		public ReflectedFloatValueAccessor reflectedAccessor;

		[ConditionalHide(new string[] { "accessType", "reflectionType" }, new object[] { AccessType.Reflected, ReflectionType.Nested })]
		public NestedReflectedFloatValueAccessor nestedReflectedAccessor;

		#endregion

		public override float GetValue()
		{
			switch (accessType)
			{
				case AccessType.Transform:
					return transformToModify.GetValue();
				case AccessType.RectTransform:
					return rectToModify.GetValue();
				case AccessType.Light:
					switch (lightAttribute)
					{
						case LightAttribute.General:
							return lightToModify.GetValue();
						case LightAttribute.Color:
							return lightColorToModify.GetValue();
						default:
							throw new NotImplementedException("No case for LightAttribute " + lightAttribute);
					}
				case AccessType.ImageColor:
					return imageToModify.GetValue();
				case AccessType.Custom:
					return customAccessor.GetValue();
				case AccessType.Reflected:
					switch (reflectionType)
					{
						case (ReflectionType.Simple):
							return reflectedAccessor.GetValue();
						case (ReflectionType.Nested):
							return nestedReflectedAccessor.GetValue();
						default:
							throw new NotImplementedException("No case for GetValue for " + reflectionType);
					}
				default:
					throw new NotImplementedException("No case for GetValue for accessType " + accessType + "!");
			}
		}

		public override void SetValue(float value)
		{
			switch (accessType)
			{
				case AccessType.Transform:
					transformToModify.SetValue(value);
					break;
				case AccessType.RectTransform:
					rectToModify.SetValue(value);
					break;
				case AccessType.Light:
					switch (lightAttribute)
					{
						case LightAttribute.General:
							lightToModify.SetValue(value);
							break;
						case LightAttribute.Color:
							lightColorToModify.SetValue(value);
							break;
						default:
							throw new NotImplementedException("No case found for LightAttribute " + lightAttribute);
					}
					break;
				case AccessType.ImageColor:
					imageToModify.SetValue(value);
					break;
				case AccessType.Custom:
					customAccessor.SetValue(value);
					break;
				case AccessType.Reflected:
					switch (reflectionType)
					{
						case (ReflectionType.Simple):
							reflectedAccessor.SetValue(value);
							break;
						case (ReflectionType.Nested):
							nestedReflectedAccessor.SetValue(value);
							break;
						default:
							throw new NotImplementedException("No case for GetValue for " + reflectionType);
					}
					break;
				default:
					throw new NotImplementedException("No case for SetValue for accessType " + accessType + "!");
			}
		}
	}
}
