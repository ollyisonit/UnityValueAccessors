using ollyisonit.UnityEditorAttributes;
using System;
using UnityEngine;

namespace ollyisonit.UnityAccessors
{
	/// <summary>
	/// Accesses a random Vector3.
	/// </summary>
	[Serializable]
	public class RandomVector3Accessor : Accessor<Vector3>
	{
		public enum VectorMode
		{
			United,
			Split
		}

		[Tooltip("Should a random Vector3 be chosen from between two corner points or should each axis be considered individually?")]
		public VectorMode mode;

		[ConditionalHide("mode", VectorMode.United)]
		public Vector3 min;
		[ConditionalHide("mode", VectorMode.United)]
		public Vector3 max;
		[ConditionalHide("mode", VectorMode.United)]
		public AnimationCurve bias;

		[ConditionalHide("mode", VectorMode.Split)]
		public RandomFloatAccessor X;
		[ConditionalHide("mode", VectorMode.Split)]
		public RandomFloatAccessor Y;
		[ConditionalHide("mode", VectorMode.Split)]
		public RandomFloatAccessor Z;

		public override void Reset(MonoBehaviour o)
		{
			base.Reset(o);
			bias = AnimationCurve.Linear(0, 0, 1, 1);
		}

		protected override Vector3 GetValue()
		{
			switch (mode)
			{
				case VectorMode.United:
					return new Vector3(RandomAccessor<float>.RandomFloat(min.x, max.x, bias),
						RandomAccessor<float>.RandomFloat(min.y, max.y, bias),
						RandomAccessor<float>.RandomFloat(min.z, max.z, bias));
				case VectorMode.Split:
					return new Vector3(X.Value, Y.Value, Z.Value);
				default:
					throw new NotImplementedException("Case not found for VectorMode " + mode);
			}
		}

		protected override void SetValue(Vector3 value)
		{
			Debug.LogWarning("Cannot set value of Random Accessor!");
		}
	}
}