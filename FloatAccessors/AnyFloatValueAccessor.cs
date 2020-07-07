using dninosores.UnityEditorAttributes;
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

		[ConditionalHide("accessType", AccessType.Transform, "Accessor")]
		public TransformFloatValueAccessor transformToModify;

		[ConditionalHide(new string[] { "accessType" }, new object[] { AccessType.Light })]
		public LightFloatValueAccessor lightToModify;

		[ConditionalHide("accessType", AccessType.RectTransform, "Accessor")]
		public RectTransformFloatValueAccessor rectToModify;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor"), 
			Tooltip("Make a script that extends CustomFloatValueAccessor and reference it here")]
		public CustomFloatValueAccessor customAccessor;

		[ConditionalHide("accessType", AccessType.ImageColor, "Accessor")]
		public ImageColorFloatValueAccessor imageToModify;

		//[Rename("Accessor"), ConditionalHide("accessType", AccessType.Reflected)]
		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedFloatValueAccessor reflectedAccessor;


		public override void Reset(GameObject o)
		{
			transformToModify = new TransformFloatValueAccessor();
			transformToModify.Reset(o);
			lightToModify = new LightFloatValueAccessor();
			lightToModify.Reset(o);
			rectToModify = new RectTransformFloatValueAccessor();
			rectToModify.Reset(o);
			customAccessor = o.GetComponent<CustomFloatValueAccessor>();
			imageToModify = new ImageColorFloatValueAccessor();
			imageToModify.Reset(o);
			reflectedAccessor = new ReflectedFloatValueAccessor();
			reflectedAccessor.Reset(o);
		}



		public override float GetValue()
		{
			switch (accessType)
			{
				case AccessType.Transform:
					return transformToModify.GetValue();
				case AccessType.RectTransform:
					return rectToModify.GetValue();
				case AccessType.Light:
					return lightToModify.GetValue();
				case AccessType.ImageColor:
					return imageToModify.GetValue();
				case AccessType.Custom:
					return customAccessor.GetValue();
				case AccessType.Reflected:
					return reflectedAccessor.GetValue();
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
					lightToModify.SetValue(value);
					break;
				case AccessType.ImageColor:
					imageToModify.SetValue(value);
					break;
				case AccessType.Custom:
					customAccessor.SetValue(value);
					break;
				case AccessType.Reflected:
					reflectedAccessor.SetValue(value);
					break;
				default:
					throw new NotImplementedException("No case for SetValue for accessType " + accessType + "!");
			}
		}
	}
}
