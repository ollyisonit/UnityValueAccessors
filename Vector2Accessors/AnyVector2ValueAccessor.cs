using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public class AnyVector2ValueAccessor : ValueAccessor<Vector2>
	{
		public enum AccessType
		{
			RectTransform,
			Custom,
			Reflected
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.RectTransform, "Accessor")]
		public RectTransformVector2ValueAccessor rect;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector2ValueAccessor cust;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector2ValueAccessor reflect;

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
			rect = new RectTransformVector2ValueAccessor();
			rect.Reset(attachedObject);
			cust = attachedObject.GetComponent<CustomVector2ValueAccessor>();
			reflect = new ReflectedVector2ValueAccessor();
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