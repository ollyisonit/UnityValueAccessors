using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	[Serializable]
	public abstract class RandomAccessor<T> : Accessor<T>
	{
		public AnimationCurve bias;
		public T min;
		public T max;

		public override void Reset(GameObject source)
		{
			base.Reset(source);
			bias = AnimationCurve.Linear(0, 0, 1, 1);
		}


		/// <summary>
		/// Gets random float between 0 and 1 according to probability curve.
		/// </summary>
		/// <returns></returns>
		protected float RandomFloat()
		{
			float raw = UnityEngine.Random.Range(0f, 1f);
			return bias.Evaluate(raw);
		}


		/// <summary>
		/// Gets random float between min and max weighted to probability curve (inclusive).
		/// </summary>
		protected float RandomFloat(float min, float max)
		{
			if (min > max)
			{
				return RandomFloat(max, min);
			}
			return min + ((max - min) * RandomFloat());
		}


		public static float RandomFloat(float min, float max, AnimationCurve bias)
		{
			if (min > max)
			{
				return RandomFloat(max, min, bias);
			}
			float raw = UnityEngine.Random.Range(0f, 1f);
			float biased = bias.Evaluate(raw);
			return min + ((max - min) * biased);
		}


		public override void SetValue(T value)
		{
			Debug.LogWarning("Cannot set value of Random Accessor!");
		}
	}
}