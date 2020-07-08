using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class AnyStringAccessor : Accessor<string>
	{
		public bool reflected;
		public enum ValueType
		{
			Custom
		}

		[ConditionalHide("reflected", true, "Accessor")]
		public ReflectedStringAccessor reflectedAccessor;

		[ConditionalHide("reflected", false)]
		public ValueType valueType;

		[ConditionalHide(new string[] { "reflected", "valueType" }, new object[] { false, ValueType.Custom }, "Accessor")]
		public CustomStringAccessor customAccessor;

		public override string GetValue()
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
			reflectedAccessor = new ReflectedStringAccessor();
			reflectedAccessor.Reset(attachedObject);
			customAccessor = attachedObject.GetComponent<CustomStringAccessor>();
		}

		public override void SetValue(string value)
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