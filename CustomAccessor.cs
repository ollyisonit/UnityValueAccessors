using UnityEngine;

namespace dninosores.UnityAccessors
{
	public abstract class CustomAccessor<T> : MonoBehaviour
	{
		public abstract T GetValue();

		public abstract void SetValue(T value);
	}
}
