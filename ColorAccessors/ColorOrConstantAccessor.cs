
using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Front-facing class combining functionality of all Color accessors with a toggle to switch between 
	/// constant and accessed values.
	/// </summary>
	[Serializable]
	public class ColorOrConstantAccessor : AnyOrConstantAccessor<Color>
	{
		public AnyColorAccessor accessorValue;

		public override Accessor<Color> AccessorValue => accessorValue;
	}
}
