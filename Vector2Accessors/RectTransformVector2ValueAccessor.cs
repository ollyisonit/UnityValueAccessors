using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public class RectTransformVector2ValueAccessor : ValueAccessor<Vector2>
	{
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

		public ValueType valueType;
		public RectTransform rect;

		public override Vector2 GetValue()
		{
			switch (valueType)
			{
				case ValueType.anchoredPosition:
					return rect.anchoredPosition;
				case ValueType.anchorMax:
					return rect.anchorMax;
				case ValueType.anchorMin:
					return rect.anchorMin;
				case ValueType.offsetMax:
					return rect.offsetMax;
				case ValueType.offsetMin:
					return rect.offsetMin;
				case ValueType.pivot:
					return rect.pivot;
				case ValueType.sizeDelta:
					return rect.sizeDelta;
				default:
					throw new NotImplementedException("Case not found for " + valueType);

			}
		}

		public override void Reset(GameObject attachedObject)
		{
			rect = attachedObject.GetComponent<RectTransform>();
		}

		public override void SetValue(Vector2 value)
		{
			switch (valueType){
				case ValueType.anchoredPosition:
					rect.anchoredPosition = value;
					break;
				case ValueType.anchorMax:
					rect.anchorMax = value;
					break;
				case ValueType.anchorMin:
					rect.anchorMin = value;
					break;
				case ValueType.offsetMax:
					rect.offsetMax = value;
					break;
				case ValueType.offsetMin:
					rect.offsetMin = value;
					break;
				case ValueType.pivot:
					rect.pivot = value;
					break;
				case ValueType.sizeDelta:
					rect.sizeDelta = value;
					break;
				default:
					throw new NotImplementedException("Case not found for " + valueType);

			}
		}
	}
}