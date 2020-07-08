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
			Constant
		}
		public bool reflected;

		[ConditionalHide("reflected", false)]
		public AccessType accessType;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Transform }, "Accessor")]
		public TransformVector3Accessor trans;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Custom }, "Accessor")]
		public CustomVector3Accessor custom;

		[ConditionalHide("reflected", true, "Accessor")]
		public ReflectedVector3Accessor reflectedAccess;


		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Constant }, "Accessor")]
		public ConstantVector3Accessor constant;

		public override Vector3 GetValue()
		{
			if (reflected)
			{
				return reflectedAccess.Value;
			}
			switch (accessType)
			{
				case AccessType.Transform:
					return trans.GetValue();
				case AccessType.Custom:
					return custom.GetValue();
				
				case AccessType.Constant:
					return constant.GetValue();
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}

		public override void Reset(GameObject attachedObject)
		{
			trans = new TransformVector3Accessor();
			trans.Reset(attachedObject);
			custom = attachedObject.GetComponent<CustomVector3Accessor>();
			reflectedAccess = new ReflectedVector3Accessor();
			reflectedAccess.Reset(attachedObject);
			constant = new ConstantVector3Accessor();
			constant.Reset(attachedObject);
			
		}

		public override void SetValue(Vector3 value)
		{
			if (reflected)
			{
				reflectedAccess.Value = value;
				return;
			}

			switch (accessType)
			{
				case AccessType.Transform:
					trans.SetValue(value);
					break;
				case AccessType.Custom:
					custom.SetValue(value);
					break;
				case AccessType.Constant:
					constant.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}