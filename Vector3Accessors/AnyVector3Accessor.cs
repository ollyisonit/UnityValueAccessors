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
			#region NESTED
			Float,
			Vector2,
			#endregion
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

		#region NESTED
		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Vector2 }, "Accessor")]
		public Vector2Vector3Accessor vector2;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Float }, "Accessor")]
		public FloatVector3Accessor Float;
		#endregion

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
				#region NESTED
				case AccessType.Vector2:
					return vector2.GetValue();
				case AccessType.Float:
					return Float.Value;
				#endregion
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
			#region NESTED
			Float = new FloatVector3Accessor();
			Float.Reset(attachedObject);
			vector2 = new Vector2Vector3Accessor();
			vector2.Reset(attachedObject);
			#endregion
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
				#region NESTED
				case AccessType.Vector2:
					vector2.SetValue(value);
					break;
				case AccessType.Float:
					Float.Value = value;
					break;
				#endregion
				case AccessType.Constant:
					constant.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}