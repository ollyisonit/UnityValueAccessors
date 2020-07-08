using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Wrapper for all Vector3Accessors that don't contain nested references to AnyVector3Accessor
	/// </summary>
	[Serializable]
	public class AnyFlatVector3Accessor : Accessor<Vector3>
	{
		public enum AccessType
		{
			Transform,
			Custom,
			Reflected
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Transform, "Accessor")]
		public TransformVector3Accessor trans;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector3Accessor custom;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector3Accessor reflected;


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
			trans = new TransformVector3Accessor();
			trans.Reset(attachedObject);
			custom = attachedObject.GetComponent<CustomVector3Accessor>();
			reflected = new ReflectedVector3Accessor();
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