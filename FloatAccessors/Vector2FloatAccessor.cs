using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses a float value from a Vector2Accessor.
	/// </summary>
	[Serializable]
	public class Vector2FloatAccessor : Accessor<float>
	{
		[Tooltip("Which axis should values be read from?")]
		public Axis2D sourceAxis;
		[Tooltip("Should values be written to the X axis?")]
		public bool setX;
		[Tooltip("Should values be written to the Y axis?")]
		public bool setY;
		[Tooltip("If an axis is not being written to, should it be replaced by a constant value?")]
		public bool constantFill;
		[ConditionalHide("constantFill", true)]
		public float fillValue;

		public AnyFlatVector2Accessor vector2;

		protected override float GetValue()
		{
			return Vector2FloatUtil.GetValue(sourceAxis, vector2.Value);
		}

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			setX = true;
			setY = false;
			constantFill = false;
			fillValue = 0;
		}

		protected override void SetValue(float value)
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
