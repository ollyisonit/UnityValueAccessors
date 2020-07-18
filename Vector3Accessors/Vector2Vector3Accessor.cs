using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses a Vector3 by converting a Vector2.
	/// </summary>
	[Serializable]
	public class Vector2Vector3Accessor : Accessor<Vector3>
	{
		[Tooltip("Which axis of the Vector3 should store the Vector2's X value?")]
		public Axis3D xValueTo;
		[Tooltip("Which axis of the Vector3 should store the Vector2's Y value?")]
		public Axis3D yValueTo;
		[Tooltip("What value should be used to fill in axes that aren't being set?")]
		public float fillConstant;

		public AnyFlatVector2Accessor vector2;



		protected override Vector3 GetValue()
		{
			Vector3 outValue = new Vector3(fillConstant, fillConstant, fillConstant);
			Vector2 orig = vector2.Value;
			Vector3FloatUtil.SetValue(xValueTo, outValue, orig.x);
			Vector3FloatUtil.SetValue(yValueTo, outValue, orig.y);
			return outValue;
		}

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			xValueTo = Axis3D.X;
			yValueTo = Axis3D.Y;
		}

		protected override void SetValue(Vector3 value)
		{
			vector2.Value = new Vector2(Vector3FloatUtil.GetValue(xValueTo, value),
				Vector3FloatUtil.GetValue(yValueTo, value));
		}
	}
}