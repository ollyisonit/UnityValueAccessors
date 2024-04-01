﻿using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Allows user to choose between using a constant variable or an accessor to get a value.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class AnyOrConstantAccessor<T> : Accessor<T>
	{
		[Tooltip("Should an accessor be used to get the value?")]
		public bool accessed = false;
		[Tooltip("Constant value")]
		public T value;

		/// <summary>
		/// Override this property to point at a public Accessor named "accessorValue" in order for property
		/// drawer to work properly.
		/// </summary>
		public abstract Accessor<T> AccessorValue { get; }

		protected override T GetValue()
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

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			accessed = false;
		}

		protected override void SetValue(T value)
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
