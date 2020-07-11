
using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Front-facing class combining functionality of all float accessors with a toggle to switch between 
	/// constant and accessed values.
	/// </summary>
	[Serializable]
	public class FloatOrConstantAccessor : AnyOrConstantAccessor<float>
	{
		public AnyFloatAccessor accessorValue;

		public override Accessor<float> AccessorValue => accessorValue;
	}
}
