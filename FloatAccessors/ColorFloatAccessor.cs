using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Gets float value by taking one channel from a ColorAccessor.
	/// </summary>
	[Serializable]
	public class ColorFloatAccessor : Accessor<float>
	{
		[Tooltip("Which color channel should the value be read from?")]
		public ColorChannel sourceChannel;
		[Tooltip("Set the red channel when SetValue is called")]
		public bool setR;
		[Tooltip("Set the green channel when SetValue is called")]
		public bool setG;
		[Tooltip("Set the blue channel when SetValue is called")]
		public bool setB;
		[Tooltip("Set the alpha channel when SetValue is called")]
		public bool setA;
		public AnyFlatColorAccessor color;

		public override float GetValue()
		{
			return ColorFloatUtil.GetChannel(color.Value, sourceChannel);
		}

		public override void Reset(GameObject attachedObject)
		{
			setR = false;
			setG = false;
			setB = false;
			setA = true;
			color = new AnyFlatColorAccessor();
			color.Reset(attachedObject);
			sourceChannel = ColorChannel.A;
		}

		public override void SetValue(float value)
		{
			Color orig = color.Value;
			color.Value = new Color(setR ? value : orig.r, setG ? value : orig.g,
				setB ? value : orig.b, setA ? value : orig.a);
		}
	}

}