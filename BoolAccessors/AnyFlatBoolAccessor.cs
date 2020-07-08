using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class AnyFlatBoolAccessor : Accessor<bool>
	{
		public bool reflected;
		public enum ValueType
		{
			Custom
		}

		[ConditionalHide("reflected", true, "Accessor")]
		public ReflectedBoolAccessor reflectedAccessor;

		[ConditionalHide("reflected", false)]
		public ValueType valueType;

		[ConditionalHide(new string[] { "reflected", "valueType" }, new object[] { false, ValueType.Custom }, "Accessor")]
		public CustomBoolAccessor customAccessor;

		public override bool GetValue()
		{
			if (reflected)
			{
				return reflectedAccessor.GetValue();
			}

			switch (valueType)
			{
				case ValueType.Custom:
					return customAccessor.GetValue();
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}

		public override void Reset(GameObject attachedObject)
		{
			throw new NotImplementedException();
		}

		public override void SetValue(bool value)
		{
			if (reflected)
			{
				reflectedAccessor.Value = value;
				return;
			}

			switch (valueType)
			{
				case ValueType.Custom:
					customAccessor.SetValue(value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}
	}
}