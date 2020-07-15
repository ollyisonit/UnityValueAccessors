using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses a random integer between two values.
	/// </summary>
	[Serializable]
	public class RandomIntAccessor : RandomAccessor<int>
	{
		public override int GetValue()
		{
			return Mathf.RoundToInt(RandomFloat(min, max));
		}


	}
}