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
			RectTransform = 0,
			#region NESTED
			Vector3 = 1,
			Float = 2,
			#endregion
			Reflected = 3,
			Custom = 4,
			Constant = 5
		}


		public AccessType accessType;

		[ConditionalHide("accessType", AccessType.RectTransform, "Accessor")]
		public RectTransformVector2Accessor rect;

		[ConditionalHide("accessType", AccessType.Custom, "Accessor")]
		public CustomVector2Accessor cust;

		[ConditionalHide("accessType", AccessType.Reflected, "Accessor")]
		public ReflectedVector2Accessor reflect;

		#region NESTED
		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Vector3 }, "Accessor")]
		public Vector3Vector2Accessor vector3;

		[ConditionalHide(new string[] { "reflected", "accessType" }, new object[] { false, AccessType.Float }, "Accessor")]
		public FloatVector2Accessor Float;
		#endregion

		[ConditionalHide("accessType", AccessType.Constant, "Accessor")]
		public ConstantVector2Accessor constant;

		public override Vector2 GetValue()
		{
			switch (accessType)
			{
				case AccessType.RectTransform:
					return rect.GetValue();
				case AccessType.Custom:
					return cust.GetValue();
				case AccessType.Constant:
					return constant.Value;
				case AccessType.Reflected:
					return reflect.Value;
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
				case AccessType.Reflected:
					reflect.Value = value;
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