
using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Front-facing class combining functionality of all Vector3 accessors with a toggle to switch between 
	/// constant and accessed values.
	/// </summary>
	[Serializable]
	public class Vector3OrConstantAccessor : AnyOrConstantAccessor<Vector3>
	{
		public AnyVector3Accessor accessorValue;

		public override Accessor<Vector3> AccessorValue => accessorValue;
	}
}
