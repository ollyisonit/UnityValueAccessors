using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class AnyVector3Accessor : Accessor<Vector3>
	{
		public enum AccessType
		{
			Transform,
			Float,
			Vector2,
			Custom,
			Reflected,
			Constant
		}

		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.Transform, "Accessor")]
		public TransformVector3Accessor trans;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector3Accessor custom;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector3Accessor reflected;

		[ConditionalHide("accessType", AccessType.Vector2, "Accessor")]
		public Vector2Vector3Accessor vector2;

		[ConditionalHide("accessType", AccessType.Float, "Accessor")]
		public FloatVector3Accessor Float;

		[ConditionalHide("accessType", AccessType.Constant, "Accessor")]
		public ConstantVector3Accessor constant;

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
				case AccessType.Vector2:
					return vector2.GetValue();
				case AccessType.Constant:
					return constant.GetValue();
				case AccessType.Float:
					return Float.Value;
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
			vector2 = new Vector2Vector3Accessor();
			vector2.Reset(attachedObject);
			constant = new ConstantVector3Accessor();
			constant.Reset(attachedObject);
			Float = new FloatVector3Accessor();
			Float.Reset(attachedObject);
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
				case AccessType.Vector2:
					vector2.SetValue(value);
					break;
				case AccessType.Constant:
					constant.SetValue(value);
					break;
				case AccessType.Float:
					Float.Value = value;
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}