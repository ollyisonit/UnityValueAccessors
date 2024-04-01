
using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Front-facing class combining functionality of all Vector2 accessors with a toggle to switch between 
	/// constant and accessed values.
	/// </summary>
	[Serializable]
	public class Vector2OrConstantAccessor : AnyOrConstantAccessor<Vector2>
	{
		public AnyVector2Accessor accessorValue;

		public override Accessor<Vector2> AccessorValue => accessorValue;
	}
}
