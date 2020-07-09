using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class TransformFloatAccessor : Accessor<float>
	{
		public enum TransformType
		{
			Position = 0,
			LocalPosition = 1,
			Rotation = 2,
			LocalRotation = 3,
			LocalScale = 4,
			LocalScaleAllAxes = 5
		}

		public Transform transform;
		public TransformType transformType;

		[ConditionalHide(new string[] { "transformType", "transformType", "transformType", "transformType", "transformType" },
			new object[]{TransformType.Position, TransformType.LocalPosition, TransformType.Rotation,
			TransformType.LocalRotation, TransformType.LocalScale}, ConditionalHideAttribute.FoldBehavior.Or)]
		public Axis3D transformAxis;

		public override float GetValue()
		{
			if (transformType == TransformType.LocalScaleAllAxes)
			{
				return Vector3FloatUtil.GetValue(transformAxis, transform.localScale);
			}
			return Vector3FloatUtil.GetValue(transformAxis, GetVector3FromTransform(transformType, transform));
		}

		public override void SetValue(float value)
		{
			if (transformType == TransformType.LocalScaleAllAxes)
			{
				transform.localScale = new Vector3(value, value, value);
				return;
			}
			SetVector3FromTransform(transformType, transform,
						Vector3FloatUtil.SetValue(transformAxis, GetVector3FromTransform(transformType, transform), value));
		}

		public Vector3 GetVector3FromTransform(TransformType ttype, Transform transform)
		{
			switch (ttype)
			{
				case TransformType.Position:
					return transform.position;
				case TransformType.LocalPosition:
					return transform.localPosition;
				case TransformType.Rotation:
					return transform.eulerAngles;
				case TransformType.LocalRotation:
					return transform.localEulerAngles;
				case TransformType.LocalScale:
					return transform.localScale;
				default:
					throw new NotImplementedException(ttype + " not implemented.");
			}
		}


		public void SetVector3FromTransform(TransformType ttype, Transform transform, Vector3 value)
		{
			switch (ttype)
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
					throw new NotImplementedException(ttype + " not implemented.");
			}
		}

		public override void Reset(GameObject attachedObject)
		{
			transform = attachedObject.transform;
		}
	}
}
