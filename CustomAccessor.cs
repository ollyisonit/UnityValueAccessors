using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Base class for user-created accessors.
	/// </summary>
	public abstract class CustomAccessor<T> : MonoBehaviour
	{
		public T Value
		{
			get
			{
				return GetValue();
			}
			set
			{
				SetValue(value);
			}
		}

		/// <summary>
		/// Gets referenced value.
		/// </summary>
		protected abstract T GetValue();


		/// <summary>
		/// Sets referenced value.
		/// </summary>
		protected abstract void SetValue(T value);
	}
}
