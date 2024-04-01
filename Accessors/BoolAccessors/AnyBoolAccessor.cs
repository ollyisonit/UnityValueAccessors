using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Accessor containing a dropdown of all existing bool accessors.
	/// </summary>
	[Serializable]
	public class AnyBoolAccessor : Accessor<bool>
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

		protected override bool GetValue()
		{

			switch (valueType)
			{
				case ValueType.Custom:
					return customAccessor.Value;
				case ValueType.Reflected:
					return reflectedAccessor.Value;
				case ValueType.Constant:
					return constant.Value;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			customAccessor = attachedObject.GetComponent<CustomBoolAccessor>();
		}

		protected override void SetValue(bool value)
		{
			switch (valueType)
			{
				case ValueType.Reflected:
					reflectedAccessor.Value = (value);
					break;
				case ValueType.Custom:
					customAccessor.Value = value;
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