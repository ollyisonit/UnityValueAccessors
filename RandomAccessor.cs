using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Gets a random value of given type.
	/// </summary>
	[Serializable]
	public abstract class RandomAccessor<T> : Accessor<T>
	{
		[Tooltip("How should the results of the random operation be distrubuted? A linear curve from (0, 0) to (1, 1) corresponds to even probability.")]
		public AnimationCurve bias;
		[Tooltip("Lowest possible value that can be returned (inclusive)")]
		public T min;
		[Tooltip("Highest possible value that can be returned (inclusive)")]
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


		/// <summary>
		/// Gets a random float between the two given values (inclusive) using given AnimationCurve as bias.
		/// Automatically reverses min and max if they're out of order.
		/// </summary>
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


		protected override void SetValue(T value)
		{
			Debug.LogWarning("Cannot set value of Random Accessor!");
		}
	}
}