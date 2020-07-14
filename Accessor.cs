using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Stores a reference to a variable of the given type.
	/// </summary>
	[Serializable]
	public abstract class Accessor<T>
	{
		/// <summary>
		/// The value that the accessor is referencing.
		/// </summary>
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
		/// Gets the value from the referenced variable.
		/// </summary>
		public abstract T GetValue();


		/// <summary>
		/// Sets the value of the referenced variable.
		/// </summary>
		public abstract void SetValue(T value);


		/// <summary>
		/// Sets Accessor to reference a sensible value when the editor is reset.
		/// </summary>
		/// <param name="attachedObject">GameObject the accessor is associated with.</param>
		public virtual void Reset(GameObject attachedObject)
		{
			ResetAccessors.Reset(this, attachedObject);
		}
	}
}
