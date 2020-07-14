using System;
using UnityEngine;
using UnityEngine.UI;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Get color from an image.
	/// </summary>
	[Serializable]
	public class ImageColorAccessor : Accessor<Color>
	{
		[Tooltip("Image to take color from")]
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