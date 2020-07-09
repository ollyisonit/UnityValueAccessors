using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class AudiosourceFloatAccessor : Accessor<float>
	{
		public AudioSource source;
		public enum ValueType
		{
			Volume = 0,
			Pitch = 1,
			MinDistance = 2,
			MaxDistance = 3,
			PanStereo = 4,
			SpatialBlend = 5,
		}

		public ValueType valueType;

		public override float GetValue()
		{
			switch (valueType)
			{
				case ValueType.Volume:
					return source.volume;
				case ValueType.Pitch:
					return source.pitch;
				case ValueType.MinDistance:
					return source.minDistance;
				case ValueType.MaxDistance:
					return source.maxDistance;
				case ValueType.PanStereo:
					return source.panStereo;
				case ValueType.SpatialBlend:
					return source.spatialBlend;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}

		public override void Reset(GameObject attachedObject)
		{
			source = attachedObject.GetComponent<AudioSource>();
		}

		public override void SetValue(float value)
		{
			switch (valueType)
			{
				case ValueType.Volume:
					source.volume = value;
					break;
				case ValueType.Pitch:
					source.pitch = value;
					break;
				case ValueType.MinDistance:
					source.minDistance = value;
					break;
				case ValueType.MaxDistance:
					source.maxDistance = value;
					break;
				case ValueType.PanStereo:
					source.panStereo = value;
					break;
				case ValueType.SpatialBlend:
					source.spatialBlend = value;
					break;
				default:
					throw new NotImplementedException("Case not found for " + valueType);
			}
		}
	}
}
