using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Base class for user-created accessors.
	/// </summary>
	public abstract class CustomAccessor<T> : MonoBehaviour
	{
		/// <summary>
		/// Gets referenced value.
		/// </summary>
		public abstract T GetValue();


		/// <summary>
		/// Sets referenced value.
		/// </summary>
		public abstract void SetValue(T value);
	}
}
