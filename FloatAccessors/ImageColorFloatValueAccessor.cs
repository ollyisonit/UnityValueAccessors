using UnityEngine;
using UnityEngine.UI;

namespace dninosores.UnityValueAccessors
{
	[System.Serializable]
	public class ImageColorFloatValueAccessor : ColorFloatValueAccessor
	{
		public Image image;

		public override void Reset(GameObject attachedObject)
		{
			image = attachedObject.GetComponent<Image>();
		}

		protected override Color GetColor()
		{
			return image.color;
		}

		protected override void SetColor(Color c)
		{
			image.color = c;
		}
	}
}
