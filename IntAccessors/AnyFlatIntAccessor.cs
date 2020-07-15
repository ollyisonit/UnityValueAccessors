using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses an integer using any integer accessor that doesn't contain a nested IntAccessor.
	/// </summary>
	[Serializable]
	public class AnyFlatIntAccessor : Accessor<int>
	{
		public enum ValueType
		{
			Reflected = 0,
			Custom = 1,
			Constant = 2,
			Random = 3
		}

		[Tooltip("Where should the value be accessed from?")]
		public ValueType valueType;

		[ConditionalHide("valueType", ValueType.Custom, "Accessor")]
		public CustomIntAccessor customAccessor;

		[ConditionalHide("valueType", ValueType.Reflected, "Accessor")]
		public ReflectedIntAccessor reflectedAccessor;

		[ConditionalHide("valueType", ValueType.Constant, "Accessor")]
		public ConstantIntAccessor constant;

		[ConditionalHide("valueType", ValueType.Random, "Accessor")]
		public RandomIntAccessor random;

		public override int GetValue()
		{
			switch (valueType)
			{
				case ValueType.Custom:
					return customAccessor.GetValue();
				case ValueType.Reflected:
					return reflectedAccessor.Value;
				case ValueType.Constant:
					return constant.Value;
				case ValueType.Random:
					return random.Value;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}


		public override void SetValue(int value)
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
				case ValueType.Random:
					random.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}
	}
}