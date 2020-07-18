using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Front-facing Value Accessor that merges functionality of all standard float accessors into a single class for serialization in the unity editor.
	/// </summary>
	[Serializable]
	public class AnyFloatAccessor : Accessor<float>
	{
		public enum AccessType
		{
			Transform = 0,
			RectTransform = 1,
			Light = 2,
			AudioSource = 3,

			#region NESTED
			Vector2 = 4,
			Vector3 = 5,
			Color = 6,
			#endregion

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


		#region NESTED

		[ConditionalHide("accessType", AccessType.Vector2, "Accessor")]
		public Vector2FloatAccessor v2;

		[ConditionalHide("accessType", AccessType.Vector3, "Accessor")]
		public Vector3FloatAccessor v3;

		[ConditionalHide("accessType", AccessType.Color, "Accessor")]
		public ColorFloatAccessor color;

		#endregion



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

				#region NESTED
				case AccessType.Vector2:
					return v2.Value;
				case AccessType.Vector3:
					return v3.Value;
				case AccessType.Color:
					return color.Value;
				#endregion

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

				#region NESTED
				case AccessType.Vector2:
					v2.Value = value;
					break;
				case AccessType.Vector3:
					v3.Value = value;
					break;
				case AccessType.Color:
					color.Value = value;
					break;
				#endregion
				case AccessType.Reflected:
					reflectedAccessor.Value = value;
					break;
				default:
					throw new NotImplementedException("No case for SetValue for accessType " + accessType + "!");
			}
		}
	}
}
