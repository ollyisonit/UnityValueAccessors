using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public abstract class Accessor<T>
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


		public abstract T GetValue();

		public abstract void SetValue(T value);

		public abstract void Reset(GameObject attachedObject);
	}
}
