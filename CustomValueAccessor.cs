using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	public abstract class CustomValueAccessor<T> : MonoBehaviour
	{
		public abstract T GetValue();

		public abstract void SetValue(T value);
	}
}
