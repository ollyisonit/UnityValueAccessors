using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class Vector2FloatAccessor : Accessor<float>
	{
		public Axis2D sourceAxis;
		public bool setX;
		public bool setY;
		public bool constantFill;
		[ConditionalHide("constantFill", true)]
		public float fillValue;

		public AnyFlatVector2Accessor vector2;

		public override float GetValue()
		{
			return Vector2FloatUtil.GetValue(sourceAxis, vector2.Value);
		}

		public override void Reset(GameObject attachedObject)
		{
			vector2 = new AnyFlatVector2Accessor();
			vector2.Reset(attachedObject);
			setX = true;
			setY = false;
			constantFill = false;
			fillValue = 0;
		}

		public override void SetValue(float value)
		{
			Vector2 original = vector2.Value;
			vector2.Value = new Vector2(FindValue(value, setX, original.x),
				FindValue(value, setY, original.y));
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
