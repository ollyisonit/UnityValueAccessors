using System;
using UnityEditor;
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
		/// The GameObject this Accessor is associated with, if applicable.
		/// </summary>
		public GameObject attachedObject;

		/// <summary>
		/// The value that the accessor is referencing.
		/// </summary>
		public T Value
		{
			get
			{
				try
				{
					return GetValue();
				}
				catch (Exception e)
				{
					if (attachedObject != null)
					{
						Debug.LogError(GetErrorPrefix() + e.Message, attachedObject);
					}
					throw e;
				}
			}
			set
			{
				try
				{
					SetValue(value);
				}
				catch (Exception e)
				{
					if (attachedObject != null)
					{
						Debug.LogError(GetErrorPrefix() + e.Message, attachedObject);
					}
					throw e;
				}
				
			}
		}


		private string GetErrorPrefix()
		{
			return "Exception on '" + attachedObject.name + "': ";
		}


		/// <summary>
		/// Gets the value from the referenced variable.
		/// </summary>
		protected abstract T GetValue();


		/// <summary>
		/// Sets the value of the referenced variable.
		/// </summary>
		protected abstract void SetValue(T value);


		/// <summary>
		/// Sets Accessor to reference a sensible value when the editor is reset. Will automatically reset any 
		/// accessors that are fields of this object. Don't forget to call base.Reset() in your override!
		/// </summary>
		/// <param name="attachedObject">GameObject the accessor is associated with.</param>
		public virtual void Reset(GameObject attachedObject)
		{
			ResetAccessors.Reset(this, attachedObject);
			this.attachedObject = attachedObject.gameObject;
		}
	}
}
