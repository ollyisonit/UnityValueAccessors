using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Wrapper for all Vector2Accessors that don't include nested references to AnyVector2Accessor.
	/// </summary>
	[Serializable]
	public class AnyFlatVector2Accessor : Accessor<Vector2>
	{
		public enum AccessType
		{
			RectTransform,
			
			Custom,
			Constant
		}

		public bool reflected;

		[ConditionalHide("reflected", false)]
		public AccessType accessType;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.RectTransform }, "Accessor")]
		public RectTransformVector2Accessor rect;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Custom }, "Accessor")]
		public CustomVector2Accessor cust;

		[ConditionalHide("reflected", true, "Accessor")]
		public ReflectedVector2Accessor reflect;

		

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Constant }, "Accessor")]
		public ConstantVector2Accessor constant;

		public override Vector2 GetValue()
		{
			if (reflected)
			{
				return reflect.Value;
			}
			switch (accessType)
			{
				case AccessType.RectTransform:
					return rect.GetValue();
				case AccessType.Custom:
					return cust.GetValue();
				case AccessType.Constant:
					return constant.Value;
			
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}


		public override void Reset(GameObject attachedObject)
		{
			rect = new RectTransformVector2Accessor();
			rect.Reset(attachedObject);
			cust = attachedObject.GetComponent<CustomVector2Accessor>();
			reflect = new ReflectedVector2Accessor();
			reflect.Reset(attachedObject);
			constant = new ConstantVector2Accessor();
			constant.Reset(attachedObject);
		}


		public override void SetValue(Vector2 value)
		{
			if (reflected)
			{
				reflect.Value = value;
				return;
			}
			switch (accessType)
			{
				case AccessType.RectTransform:
					rect.SetValue(value);
					break;
				case AccessType.Custom:
					cust.SetValue(value);
					break;
				case AccessType.Constant:
					constant.Value = value;
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}