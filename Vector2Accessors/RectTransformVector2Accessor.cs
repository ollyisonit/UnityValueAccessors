using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Gets a Vector2 from a RectTransform.
	/// </summary>
	[Serializable]
	public class RectTransformVector2Accessor : Accessor<Vector2>
	{
		public enum ValueType
		{
			anchoredPosition = 0,
			anchorMax = 1,
			anchorMin = 2,
			offsetMax = 3,
			offsetMin =4,
			pivot =5,
			sizeDelta =6
		}

		[Tooltip("Which field should the value be accessed from?")]
		public ValueType valueType;
		public RectTransform rect;

		protected override Vector2 GetValue()
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

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			rect = attachedObject.GetComponent<RectTransform>();
		}

		protected override void SetValue(Vector2 value)
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