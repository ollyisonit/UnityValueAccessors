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
			#region NESTED
			Vector3,
			Float,
			#endregion
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

		#region NESTED
		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Vector3 }, "Accessor")]
		public Vector3Vector2Accessor vector3;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Float }, "Accessor")]
		public FloatVector2Accessor Float;
		#endregion

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
				#region NESTED
				case AccessType.Vector3:
					return vector3.Value;
				case AccessType.Float:
					return Float.Value;
				#endregion
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
			#region NESTED
			vector3 = new Vector3Vector2Accessor();
			vector3.Reset(attachedObject);
			Float = new FloatVector2Accessor();
			Float.Reset(attachedObject);
			#endregion
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
				#region NESTED
				case AccessType.Vector3:
					vector3.Value = value;
					break;
				case AccessType.Float:
					Float.Value = value;
					break;
				#endregion
				default:
					throw new NotImplementedException("Case not found for " + accessType);
			}
		}
	}
}