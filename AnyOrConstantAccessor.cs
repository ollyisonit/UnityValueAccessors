using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public abstract class AnyOrConstantAccessor<T> : Accessor<T>
	{
		public bool accessed = false;
		public T value;
		/// <summary>
		/// Override this property to point at a public Accessor named "accessorValue" in order for property
		/// drawer to work properly.
		/// </summary>
		public abstract Accessor<T> AccessorValue { get; }

		public override T GetValue()
		{
			if (!accessed)
			{
				return value;
			}
			else
			{
				return AccessorValue.Value;
			}

		}

		public override void Reset(GameObject attachedObject)
		{
			base.Reset(attachedObject);
			accessed = false;
		}

		public override void SetValue(T value)
		{
			if (!accessed)
			{
				this.value = value;
			}
			else
			{
				AccessorValue.Value = value;
			}
		}
	}
}
