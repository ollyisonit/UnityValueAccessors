using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public class RectTransformFloatValueAccessor : ValueAccessor<float>
	{
		public RectTransform rectTransform;
		public ValueType valueType;
		public Axis2D axis;

		public enum ValueType
		{
			anchoredPosition,
			anchorMax,
			anchorMin,
			offsetMax,
			offsetMin,
			pivot,
			sizeDelta
		}


		private float GetVector(Vector2 v)
		{
			return Vector2FloatAccessor.GetValue(axis, v);
		}


		private Vector2 SetVector(Vector2 v, float value)
		{
			return Vector2FloatAccessor.SetValue(axis, v, value);
		}

		public override float GetValue()
		{
			switch (valueType)
			{
				case ValueType.anchoredPosition:
					return GetVector(rectTransform.anchoredPosition);
				case ValueType.anchorMax:
					return GetVector(rectTransform.anchorMax);
				case ValueType.anchorMin:
					return GetVector(rectTransform.anchorMin);
				case ValueType.offsetMax:
					return GetVector(rectTransform.offsetMax);
				case ValueType.offsetMin:
					return GetVector(rectTransform.offsetMin);
				case ValueType.pivot:
					return GetVector(rectTransform.pivot);
				case ValueType.sizeDelta:
					return GetVector(rectTransform.sizeDelta);
				default:
					throw new NotImplementedException("Case not found for ValueType " + valueType);
			}
		}

		public override void SetValue(float value)
		{
			Vector2 Set(Vector2 original)
			{
				return SetVector(original, value);
			}

			switch (valueType)
			{
				case ValueType.anchoredPosition:
					rectTransform.anchoredPosition = Set(rectTransform.anchoredPosition);
					break;
				case ValueType.anchorMax:
					rectTransform.anchorMax = Set(rectTransform.anchorMax);
					break;
				case ValueType.anchorMin:
					rectTransform.anchorMin = Set(rectTransform.anchorMin);
					break;
				case ValueType.offsetMax:
					rectTransform.offsetMax = Set(rectTransform.offsetMax);
					break;
				case ValueType.offsetMin:
					rectTransform.offsetMin = Set(rectTransform.offsetMin);
					break;
				case ValueType.pivot:
					rectTransform.pivot = Set(rectTransform.pivot);
					break;
				case ValueType.sizeDelta:
					rectTransform.sizeDelta = Set(rectTransform.sizeDelta);
					break;
				default:
					throw new NotImplementedException("Case not found for ValueType " + valueType);
			}
		}
	}
}
