using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class Vector2Vector3Accessor : Accessor<Vector3>
	{
		public AnyVector2Accessor vector2;
		
		public override Vector3 GetValue()
		{
			throw new System.NotImplementedException();
		}

		public override void Reset(GameObject attachedObject)
		{
			//throw new System.NotImplementedException();
		}

		public override void SetValue(Vector3 value)
		{
			throw new System.NotImplementedException();
		}
	}
}