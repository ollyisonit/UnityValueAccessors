
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
	public class StringOrConstantAccessor : AnyOrConstantAccessor<string>
	{
		public AnyStringAccessor accessorValue;

		public override Accessor<string> AccessorValue => accessorValue;
	}
}
