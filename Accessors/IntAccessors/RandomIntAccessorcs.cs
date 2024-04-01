using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Accesses a random integer between two values.
	/// </summary>
	[Serializable]
	public class RandomIntAccessor : RandomAccessor<int>
	{
		protected override int GetValue()
		{
			return Mathf.RoundToInt(RandomFloat(min, max));
		}


	}
}