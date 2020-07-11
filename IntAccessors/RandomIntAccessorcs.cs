using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public class RandomIntAccessor : RandomAccessor<int>
	{
		public override int GetValue()
		{
			return Mathf.RoundToInt(RandomFloat(min, max));
		}


	}
}