using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Gets a random float value.
	/// </summary>
	[Serializable]
	public class RandomFloatAccessor : RandomAccessor<float>
	{
		protected override float GetValue()
		{
			return RandomFloat(min, max);
		}

	}
}