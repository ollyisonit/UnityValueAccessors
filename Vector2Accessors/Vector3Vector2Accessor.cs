using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class Vector3Vector2Accessor : Accessor<Vector2>
	{
		public Axis3D xSource;
		public Axis3D ySource;
		public AnyFlatVector3Accessor vector;


		public override Vector2 GetValue()
		{
			Vector3 v3 = vector.GetValue();
			return new Vector2(Vector3FloatAccessor.GetValue(xSource, v3), Vector3FloatAccessor.GetValue(ySource, v3));
		}

		public override void Reset(GameObject attachedObject)
		{
			xSource = Axis3D.X;
			ySource = Axis3D.Y;
			vector = new AnyFlatVector3Accessor();
			vector.Reset(attachedObject);
		}


		public override void SetValue(Vector2 value)
		{
			Vector3 target = vector.Value;
			Vector3FloatAccessor.SetValue(xSource, target, value.x);
			Vector3FloatAccessor.SetValue(ySource, target, value.y);
			vector.Value = target;
		}
	}

}