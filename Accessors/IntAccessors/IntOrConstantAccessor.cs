
using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Front-facing class combining functionality of all bool accessors with a toggle to switch between 
	/// constant and accessed values.
	/// </summary>
	[Serializable]
	public class IntOrConstantAccessor : AnyOrConstantAccessor<int>
	{
		public AnyIntAccessor accessorValue;

		public override Accessor<int> AccessorValue => accessorValue;
	}
}
