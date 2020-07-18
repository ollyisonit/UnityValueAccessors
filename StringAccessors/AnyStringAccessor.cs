using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses a string using any existing StringAccessor.
	/// </summary>
	[Serializable]
	public class AnyStringAccessor : Accessor<string>
	{
		public enum ValueType
		{
			Reflected = 0,
			Custom = 1,
			Constant = 2
		}

		[Tooltip("Where should the value be accessed from?")]
		public ValueType valueType;

		[ConditionalHide("valueType", ValueType.Custom, "Accessor")]
		public CustomStringAccessor customAccessor;

		[ConditionalHide("valueType", ValueType.Reflected, "Accessor")]
		public ReflectedStringAccessor reflectedAccessor;

		[ConditionalHide("valueType", ValueType.Constant, "Accessor")]
		public ConstantStringAccessor constant;

		protected override string GetValue()
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

		public override void Reset(GameObject attachedObject)
		{
			base.Reset(attachedObject);
			customAccessor = attachedObject.GetComponent<CustomStringAccessor>();
		}

		protected override void SetValue(string value)
		{
			switch (valueType)
			{
				case ValueType.Custom:
					customAccessor.Value = value;
					break;
				case ValueType.Reflected:
					reflectedAccessor.Value = value;
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