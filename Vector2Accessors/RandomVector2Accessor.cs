using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses a random Vector2 within a given range.
	/// </summary>
	[Serializable]
	public class RandomVector2Accessor : Accessor<Vector2>
	{
		public enum VectorMode
		{
			United,
			Split
		}

		[Tooltip("Should a random Vector2 be taken from between two corner points, or should each axis be generated individually?")]
		public VectorMode mode;

		[ConditionalHide("mode", VectorMode.United)]
		public Vector2 min;
		[ConditionalHide("mode", VectorMode.United)]
		public Vector2 max;
		[ConditionalHide("mode", VectorMode.United)]
		public AnimationCurve bias;

		[ConditionalHide("mode", VectorMode.Split)]
		public RandomFloatAccessor X;
		[ConditionalHide("mode", VectorMode.Split)]
		public RandomFloatAccessor Y;

		public override void Reset(MonoBehaviour o)
		{
			base.Reset(o);
			bias = AnimationCurve.Linear(0, 0, 1, 1);
		}

		protected override Vector2 GetValue()
		{
			switch (mode)
			{
				case VectorMode.United:
					return new Vector2(RandomAccessor<float>.RandomFloat(min.x, max.x, bias),
						RandomAccessor<float>.RandomFloat(min.y, max.y, bias));
				case VectorMode.Split:
					return new Vector2(X.Value, Y.Value);
				default:
					throw new NotImplementedException("Case not found for VectorMode " + mode);
			}
		}

		protected override void SetValue(Vector2 value)
		{
			Debug.LogWarning("Cannot set value of Random Accessor!");
		}
	}
}