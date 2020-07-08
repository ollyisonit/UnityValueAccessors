using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Value Accessor that merges functionality of all standard float accessors into a single class for serialization in the unity editor.
	/// </summary>
	[Serializable]
	public class AnyFloatAccessor : Accessor<float>
	{
		public enum AccessType
		{
			Transform,
			RectTransform,
			Light,
			AudioSource,

			#region NESTED
			Vector2,
			Vector3,
			Color,
			#endregion

			Custom,
			Constant
		}

		public bool reflected;
		[ConditionalHide("reflected", false)]
		public AccessType accessType;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Transform }, "Accessor")]
		public TransformFloatAccessor transformToModify;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Light }, "Accessor")]
		public LightFloatAccessor lightToModify;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.AudioSource }, "Accessor")]
		public AudiosourceFloatAccessor audio;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.RectTransform }, "Accessor")]
		public RectTransformFloatAccessor rectToModify;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Custom }, "Accessor"),
		Tooltip("Make a script that extends CustomFloatAccessor and reference it here")]
		public CustomFloatAccessor customAccessor;

		[ConditionalHide("reflected", true, "Accessor")]
		public ReflectedFloatAccessor reflectedAccessor;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Constant }, "Accessor")]
		public ConstantFloatAccessor constant;


		#region NESTED
		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Vector2 }, "Accessor")]
		public Vector2FloatAccessor v2;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Vector3 }, "Accessor")]
		public Vector3FloatAccessor v3;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Color }, "Accessor")]
		public ColorFloatAccessor color;
		#endregion

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


			#region NESTED
			v2 = new Vector2FloatAccessor();
			v2.Reset(o);
			v3 = new Vector3FloatAccessor();
			v3.Reset(o);
			color = new ColorFloatAccessor();
			color.Reset(o);
			#endregion
		}



		public override float GetValue()
		{
			if (reflected)
			{
				return reflectedAccessor.Value;
			}
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

				#region NESTED
				case AccessType.Vector2:
					return v2.Value;
				case AccessType.Vector3:
					return v3.Value;
				case AccessType.Color:
					return color.Value;
				#endregion

				default:
					throw new NotImplementedException("No case for GetValue for accessType " + accessType + "!");
			}
		}

		public override void SetValue(float value)
		{
			if (reflected)
			{
				reflectedAccessor.Value = value;
				return;
			}
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
				default:
					throw new NotImplementedException("No case for SetValue for accessType " + accessType + "!");
			}
		}
	}
}
