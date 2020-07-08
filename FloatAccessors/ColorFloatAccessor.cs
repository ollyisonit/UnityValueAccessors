using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class ColorFloatAccessor : Accessor<float>
	{	
		public ColorChannel sourceChannel;
		public bool setR;
		public bool setG;
		public bool setB;
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