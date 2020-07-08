using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public class AnyVector3ValueAccessor : ValueAccessor<Vector3>
	{
		public enum AccessType
		{
			Transform,
			Custom,
			Reflected
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Transform, "Accessor")]
		public TransformVector3ValueAccessor trans;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector3ValueAccessor custom;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector3ValueAccessor reflected;


		public override Vector3 GetValue()
		{
			switch (accessType)
			{
				case AccessType.Transform:
					return trans.GetValue();
				case AccessType.Custom:
					return custom.GetValue();
				case AccessType.Reflected:
					return reflected.GetValue();
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}

		public override void Reset(GameObject attachedObject)
		{
			trans = new TransformVector3ValueAccessor();
			trans.Reset(attachedObject);
			custom = attachedObject.GetComponent<CustomVector3ValueAccessor>();
			reflected = new ReflectedVector3ValueAccessor();
			reflected.Reset(attachedObject);
		}

		public override void SetValue(Vector3 value)
		{
			switch (accessType)
			{
				case AccessType.Transform:
					trans.SetValue(value);
					break;
				case AccessType.Custom:
					custom.SetValue(value);
					break;
				case AccessType.Reflected:
					reflected.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}