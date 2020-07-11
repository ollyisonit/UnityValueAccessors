
using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Front-facing class combining functionality of all string accessors with a toggle to switch between 
	/// constant and accessed values.
	/// </summary>
	[Serializable]
	public class StringOrConstantAccessor : AnyOrConstantAccessor<string>
	{
		public AnyStringAccessor accessorValue;

		public override Accessor<string> AccessorValue => accessorValue;
	}
}
