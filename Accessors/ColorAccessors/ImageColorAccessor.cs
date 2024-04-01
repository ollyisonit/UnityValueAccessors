using System;
using UnityEngine;
using UnityEngine.UI;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Get color from an image.
	/// </summary>
	[Serializable]
	public class ImageColorAccessor : Accessor<Color>
	{
		[Tooltip("Image to take color from")]
		public Image image;

		protected override Color GetValue()
		{
			return image.color;
		}

		public override void Reset(MonoBehaviour attachedObject)
		{
			base.Reset(attachedObject);
			image = attachedObject.GetComponent<Image>();
		}

		protected override void SetValue(Color value)
		{
			image.color = value;
		}
	}

}