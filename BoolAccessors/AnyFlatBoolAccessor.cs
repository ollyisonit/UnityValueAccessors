using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accessor containing a dropdown of all BoolAccessors that don't reference other BoolAccessors.
	/// </summary>
	[Serializable]
	public class AnyFlatBoolAccessor : Accessor<bool>
	{
		public enum ValueType
		{
			Reflected = 0,
			Custom = 1,
			Constant = 2
		}

		[Tooltip("Where should the value be accessed from?")]
		public ValueType valueType;

		[ConditionalHide("valueType", ValueType.Reflected, "Accessor")]
		public ReflectedBoolAccessor reflectedAccessor;

		[ConditionalHide("valueType", ValueType.Custom, "Accessor")]
		public CustomBoolAccessor customAccessor;

		[ConditionalHide("valueType", ValueType.Constant, "Accessor")]
		public ConstantBoolAccessor constant;

		public override bool GetValue()
		{

			switch (valueType)
			{
				case ValueType.Custom:
					return customAccessor.GetValue();
				case ValueType.Reflected:
					return reflectedAccessor.GetValue();
				case ValueType.Constant:
					return constant.Value;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}

		public override void Reset(GameObject attachedObject)
		{
			reflectedAccessor = new ReflectedBoolAccessor();
			reflectedAccessor.Reset(attachedObject);
			customAccessor = attachedObject.GetComponent<CustomBoolAccessor>();
			constant = new ConstantBoolAccessor();
			constant.Reset(attachedObject);
		}

		public override void SetValue(bool value)
		{
			switch (valueType)
			{
				case ValueType.Reflected:
					reflectedAccessor.SetValue(value);
					break;
				case ValueType.Custom:
					customAccessor.SetValue(value);
					break;
				case ValueType.Constant:
					constant.Value = value;
					break;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}
	}
}