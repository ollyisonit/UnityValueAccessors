using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses a float from a Vector3Accessor.
	/// </summary>
	[Serializable]
	public class Vector3FloatAccessor : Accessor<float>
	{
		[Tooltip("Which axis should values be read from?")]
		public Axis3D sourceAxis;
		[Tooltip("Should values be written to the X axis?")]
		public bool setX;
		[Tooltip("Should values be written to the Y axis?")]
		public bool setY;
		[Tooltip("Should values be written to the Z axis?")]
		public bool setZ;
		[Tooltip("Should values that aren't being written be replaced with a constant value?")]
		public bool constantFill;
		[ConditionalHide("constantFill", true)]
		public float fillValue;

		public AnyFlatVector3Accessor vector3;

		protected override float GetValue()
		{
			return Vector3FloatUtil.GetValue(sourceAxis, vector3.Value);
		}

		public override void Reset(GameObject attachedObject)
		{
			base.Reset(attachedObject);
			setX = true;
			setY = false;
			setZ = false;
			constantFill = false;
			fillValue = 0;
		}

		protected override void SetValue(float value)
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
