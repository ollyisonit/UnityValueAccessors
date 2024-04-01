using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
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



			Reflected = 7,
			Custom = 8,
			Constant = 9,
			Random = 10
		}

		[Tooltip("Where should the value be accessed from?")]
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

		[ConditionalHide("accessType", AccessType.Random, "Accessor")]
		public RandomFloatAccessor random;




		protected override float GetValue()
		{
			switch (accessType)
			{
				case AccessType.Transform:
					return transformToModify.Value;
				case AccessType.RectTransform:
					return rectToModify.Value;
				case AccessType.Light:
					return lightToModify.Value;
				case AccessType.Custom:
					return customAccessor.Value;
				case AccessType.Constant:
					return constant.Value;
				case AccessType.AudioSource:
					return audio.Value;
				case AccessType.Random:
					return random.Value;


				case AccessType.Reflected:
					return reflectedAccessor.Value;

				default:
					throw new NotImplementedException("No case for GetValue for accessType " + accessType + "!");
			}
		}

		protected override void SetValue(float value)
		{
			switch (accessType)
			{
				case AccessType.Transform:
					transformToModify.Value = value;
					break;
				case AccessType.RectTransform:
					rectToModify.Value = value;
					break;
				case AccessType.Light:
					lightToModify.Value = value;
					break;
				case AccessType.Custom:
					customAccessor.Value = value;
					break;
				case AccessType.Constant:
					constant.Value = value;
					break;
				case AccessType.AudioSource:
					audio.Value = value;
					break;
				case AccessType.Random:
					random.Value = value;
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
