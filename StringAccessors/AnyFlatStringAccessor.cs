using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class AnyFlatStringAccessor : Accessor<string>
	{
		public enum ValueType
		{
			Reflected = 0,
			Custom = 1,
			Constant = 2
		}

		public ValueType valueType;

		[ConditionalHide("valueType", ValueType.Custom, "Accessor")]
		public CustomStringAccessor customAccessor;

		[ConditionalHide("valueType", ValueType.Reflected, "Accessor")]
		public ReflectedStringAccessor reflectedAccessor;

		[ConditionalHide("valueType", ValueType.Constant, "Accessor")]
		public ConstantStringAccessor constant;

		public override string GetValue()
		{
			switch (valueType)
			{
				case ValueType.Custom:
					return customAccessor.GetValue();
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
			reflectedAccessor = new ReflectedStringAccessor();
			reflectedAccessor.Reset(attachedObject);
			customAccessor = attachedObject.GetComponent<CustomStringAccessor>();
			constant = new ConstantStringAccessor();
			constant.Reset(attachedObject);
		}

		public override void SetValue(string value)
		{
			switch (valueType)
			{
				case ValueType.Custom:
					customAccessor.SetValue(value);
					break;
				case ValueType.Reflected:
					reflectedAccessor.SetValue(value);
					break;
				case ValueType.Constant:
					constant.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}
	}
}