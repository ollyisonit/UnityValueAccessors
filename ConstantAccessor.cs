using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Stores a reference to a constant value.
	/// </summary>
	[Serializable]
	public class ConstantAccessor<T> : Accessor<T>
	{
		[Tooltip("Constant value that is being referenced")]
		public T value;
		[Tooltip("Should other scripts not be allowed to change the value stored here?")]
		public bool readOnly;

		protected override T GetValue()
		{
			return value;
		}

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			readOnly = true;
		}

		protected override void SetValue(T value)
		{
			if (!readOnly)
			{
				this.value = value;
			}
		}
	}

}