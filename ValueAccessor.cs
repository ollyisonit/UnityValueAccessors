using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public abstract class ValueAccessor<T>
	{
		public abstract T GetValue();

		public abstract void SetValue(T value);

		public abstract void Reset(GameObject attachedObject);
	}
}
