using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class Vector3FloatAccessor : Accessor<float>
	{
		public Axis3D sourceAxis;
		public bool setX;
		public bool setY;
		public bool setZ;
		public bool constantFill;
		[ConditionalHide("constantFill", true)]
		public float fillValue;

		public AnyFlatVector3Accessor vector3;

		public override float GetValue()
		{
			return Vector3FloatUtil.GetValue(sourceAxis, vector3.Value);
		}

		public override void Reset(GameObject attachedObject)
		{
			vector3 = new AnyFlatVector3Accessor();
			vector3.Reset(attachedObject);
			setX = true;
			setY = false;
			setZ = false;
			constantFill = false;
			fillValue = 0;
		}

		public override void SetValue(float value)
		{
			Vector3 original = vector3.Value;
			vector3.Value = new Vector3(FindValue(value, setX, original.x),
				FindValue(value, setY, original.y), FindValue(value, setZ, original.z));
		}


		private float FindValue(float value, bool shouldSet, float original)
		{
			if (shouldSet)
			{
				return value;
			}
			else if (constantFill)
			{
				return fillValue;
			}
			else
			{
				return original;
			}
		}
	}
}
