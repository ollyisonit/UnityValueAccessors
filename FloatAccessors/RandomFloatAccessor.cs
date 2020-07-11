using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class RandomFloatAccessor : RandomAccessor<float>
	{
		public override float GetValue()
		{
			return RandomFloat(min, max);
		}

	}
}