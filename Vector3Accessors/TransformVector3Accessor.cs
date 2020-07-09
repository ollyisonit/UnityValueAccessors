using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class TransformVector3Accessor : Accessor<Vector3>
	{
		public enum TransformType
		{
			Position = 0,
			LocalPosition = 1,
			Rotation = 2,
			LocalRotation = 3,
			LocalScale = 4,
		}

		public Transform transform;
		public TransformType transformType;

		public override Vector3 GetValue()
		{
			switch (transformType)
			{
				case TransformType.Position:
					return transform.position;
				case TransformType.LocalPosition:
					return transform.localPosition;
				case TransformType.Rotation:
					return transform.rotation.eulerAngles;
				case TransformType.LocalRotation:
					return transform.localRotation.eulerAngles;
				case TransformType.LocalScale:
					return transform.localScale;
				default:
					throw new NotImplementedException("Case not found for " + transformType);
			}
		}

		public override void Reset(GameObject attachedObject)
		{
			transform = attachedObject.transform;
		}

		public override void SetValue(Vector3 value)
		{
			switch (transformType)
			{
				case TransformType.Position:
					transform.position = value;
					break;
				case TransformType.LocalPosition:
					transform.localPosition = value;
					break;
				case TransformType.Rotation:
					transform.rotation = Quaternion.identity;
					transform.Rotate(value, Space.World);
					break;
				case TransformType.LocalRotation:
					transform.localRotation = Quaternion.identity;
					transform.Rotate(value, Space.Self);
					break;
				case TransformType.LocalScale:
					transform.localScale = value;
					break;
				default:
					throw new NotImplementedException("Case not found for " + transformType);
			}
		}
	}
}