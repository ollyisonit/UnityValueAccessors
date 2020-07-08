using System;
using UnityEngine;
using UnityEngine.UI;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class ImageColorAccessor : Accessor<Color>
	{
		public Image image;

		public override Color GetValue()
		{
			return image.color;
		}

		public override void Reset(GameObject attachedObject)
		{
			image = attachedObject.GetComponent<Image>();
		}

		public override void SetValue(Color value)
		{
			image.color = value;
		}
	}

}