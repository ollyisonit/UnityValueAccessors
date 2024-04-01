using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Gets float value from a light.
	/// </summary>
	[Serializable]
	public class LightFloatAccessor : Accessor<float>
	{
		public Light light;
		[Tooltip("Where should the value be accessed from?")]
		public ValueType valueType;
		public enum ValueType
		{
			Intensity = 0,
			Range = 1,
			Color = 2,
			SpotAngle = 3,
			InnerSpotAngle = 4,
			BounceIntensity = 6,
			ColorTemperature = 5
		}


		[ConditionalHide("valueType", ValueType.Color)]
		public ColorChannel channel;


		protected override float GetValue()
		{
			switch (valueType)
			{
				case ValueType.InnerSpotAngle:
					return light.innerSpotAngle;
				case ValueType.Range:
					return light.range;
				case ValueType.Color:
					return ColorFloatUtil.GetChannel(light.color, channel);
				case ValueType.SpotAngle:
					return light.spotAngle;
				case ValueType.Intensity:
					return light.intensity;
				case ValueType.BounceIntensity:
					return light.bounceIntensity;
				case ValueType.ColorTemperature:
					return light.colorTemperature;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}

		protected override void SetValue(float value)
		{
			switch (valueType)
			{
				case ValueType.InnerSpotAngle:
					light.innerSpotAngle = value;
					break;
				case ValueType.Range:
					light.range = value;
					break;
				case ValueType.Color:
					light.color = ColorFloatUtil.SetChannel(light.color, channel, value);
					break;
				case ValueType.SpotAngle:
					light.spotAngle = value;
					break;
				case ValueType.Intensity:
					light.intensity = value;
					break;
				case ValueType.BounceIntensity:
					light.bounceIntensity = value;
					break;
				case ValueType.ColorTemperature:
					light.colorTemperature = value;
					break;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			light = attachedObject.GetComponent<Light>();
		}
	}
}
