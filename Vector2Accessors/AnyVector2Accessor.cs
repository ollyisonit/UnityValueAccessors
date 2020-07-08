using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class AnyVector2Accessor : Accessor<Vector2>
	{
		public enum AccessType
		{
			RectTransform,
			Custom,
			Reflected
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.RectTransform, "Accessor")]
		public RectTransformVector2Accessor rect;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector2Accessor cust;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector2Accessor reflect;

		public override Vector2 GetValue()
		{
			switch (accessType)
			{
				case AccessType.RectTransform:
					return rect.GetValue();
				case AccessType.Custom:
					return cust.GetValue();
				case AccessType.Reflected:
					return reflect.GetValue();
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
		}


		public override void SetValue(Vector2 value)
		{
			switch (accessType)
			{
				case AccessType.RectTransform:
					rect.SetValue(value);
					break;
				case AccessType.Custom:
					cust.SetValue(value);
					break;
				case AccessType.Reflected:
					reflect.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}