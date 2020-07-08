using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class ConstantAccessor<T> : Accessor<T>
	{
		public T value;
		public bool readOnly;

		public override T GetValue()
		{
			return value;
		}

		public override void Reset(GameObject attachedObject)
		{
			readOnly = true;
		}

		public override void SetValue(T value)
		{
			if (!readOnly)
			{
				this.value = value;
			}
		}
	}

}