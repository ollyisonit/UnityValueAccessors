using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Gets a random float value.
	/// </summary>
	[Serializable]
	public class RandomFloatAccessor : RandomAccessor<float>
	{
		public override float GetValue()
		{
			return RandomFloat(min, max);
		}

	}
}