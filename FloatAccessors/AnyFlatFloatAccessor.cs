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
			Transform = 0,
			RectTransform = 1,
			Light = 2,
			AudioSource = 3,

			Reflected = 4,
			Custom = 5,
			Constant = 6
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Transform, "Accessor")]
		public TransformFloatAccessor transformToModify;

		[ConditionalHide("accessType", AccessType.Light, "Accessor")]
		public LightFloatAccessor lightToModify;

		[ConditionalHide("accessType", AccessType.AudioSource, "Accessor")]
		public AudiosourceFloatAccessor audio;

		[ConditionalHide("accessType", AccessType.RectTransform, "Accessor")]
		public RectTransformFloatAccessor rectToModify;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor"),
		Tooltip("Make a script that extends CustomFloatAccessor and reference it here")]
		public CustomFloatAccessor customAccessor;

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
			reflectedAccessor = new ReflectedFloatAccessor();
			reflectedAccessor.Reset(o);
			constant = new ConstantFloatAccessor();
			constant.Reset(o);
			audio = new AudiosourceFloatAccessor();
			audio.Reset(o);
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
				case AccessType.Custom:
					return customAccessor.GetValue();
				case AccessType.Constant:
					return constant.Value;
				case AccessType.AudioSource:
					return audio.Value;

				case AccessType.Reflected:
					return reflectedAccessor.Value;

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
				case AccessType.Custom:
					customAccessor.SetValue(value);
					break;
				case AccessType.Constant:
					constant.Value = value;
					break;
				case AccessType.AudioSource:
					audio.Value = value;
					break;
				case AccessType.Reflected:
					reflectedAccessor.Value = value;
					break;
				default:
					throw new NotImplementedException("No case for SetValue for accessType " + accessType + "!");
			}
		}
	}

}
