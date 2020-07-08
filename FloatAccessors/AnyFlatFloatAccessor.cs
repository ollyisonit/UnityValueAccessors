using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Value Accessor that merges functionality of all standard float accessors into a single class for serialization in the unity editor.
	/// Excludes float accessors that involve nested references to other AnyFloatAccessors.
	/// </summary>
	[Serializable]
	public class AnyFlatFloatAccessor : Accessor<float>
	{
		public enum AccessType
		{
			Transform,
			RectTransform,
			Light,
			ImageColor,
			Custom,
			Reflected,
			Constant
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Transform, "Accessor")]
		public TransformFloatAccessor transformToModify;

		[ConditionalHide(new string[] { "accessType" }, new object[] { AccessType.Light })]
		public LightFloatAccessor lightToModify;

		[ConditionalHide("accessType", AccessType.RectTransform, "Accessor")]
		public RectTransformFloatAccessor rectToModify;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor"),
			Tooltip("Make a script that extends CustomFloatAccessor and reference it here")]
		public CustomFloatAccessor customAccessor;

		[ConditionalHide("accessType", AccessType.ImageColor, "Accessor")]
		public ImageColorFloatAccessor imageToModify;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedFloatAccessor reflectedAccessor;

		[ConditionalHide("accessType", AccessType.Constant, "Accessor")]
		public ConstantFloatAccessor constant;


		public override void Reset(GameObject o)
		{
			transformToModify = new TransformFloatAccessor();
			transformToModify.Reset(o);
			lightToModify = new LightFloatAccessor();
			lightToModify.Reset(o);
			rectToModify = new RectTransformFloatAccessor();
			rectToModify.Reset(o);
			customAccessor = o.GetComponent<CustomFloatAccessor>();
			imageToModify = new ImageColorFloatAccessor();
			imageToModify.Reset(o);
			reflectedAccessor = new ReflectedFloatAccessor();
			reflectedAccessor.Reset(o);
			constant = new ConstantFloatAccessor();
			constant.Reset(o);
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
				case AccessType.Constant:
					return constant.Value;
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
				case AccessType.Constant:
					constant.Value = value;
					break;
				default:
					throw new NotImplementedException("No case for SetValue for accessType " + accessType + "!");
			}
		}
	}
}
