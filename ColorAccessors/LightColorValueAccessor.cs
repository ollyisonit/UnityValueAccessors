using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public class LightColorValueAccessor : ValueAccessor<Color>
	{
		public Light light;

		public override Color GetValue()
		{
			return light.color;
		}

		public override void Reset(GameObject attachedObject)
		{
			light = attachedObject.GetComponent<Light>();
		}

		public override void SetValue(Color value)
		{
			light.color = value;
		}
	}
}