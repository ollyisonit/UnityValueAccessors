using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Get color from a light.
	/// </summary>
	[Serializable]
	public class LightColorAccessor : Accessor<Color>
	{
		[Tooltip("Light to get color from")]
		public Light light;

		protected override Color GetValue()
		{
			return light.color;
		}

		public override void Reset(GameObject attachedObject)
		{
			base.Reset(attachedObject);
			light = attachedObject.GetComponent<Light>();
		}

		protected override void SetValue(Color value)
		{
			light.color = value;
		}
	}
}