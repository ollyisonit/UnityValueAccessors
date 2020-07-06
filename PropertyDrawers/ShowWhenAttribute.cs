//Adapted from https://gist.github.com/deebrol/02f61b7611fd4eca923776077b92dfc2

using System;
using UnityEngine;

namespace dninosores.UnityValueAccessors.PropertyDrawers
{
	/// <summary>
	/// Attribute used to show or hide the Field depending on certain conditions
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class ShowWhenAttribute : PropertyAttribute
	{

		public readonly string[] conditionFieldName;
		public readonly object[] comparationValueArray;

		/// <summary>
		/// Attribute used to show or hide the Field depending on certain conditions
		/// </summary>
		/// <param name="conditionFieldName">Name of the bool condition Field</param>
		public ShowWhenAttribute(string conditionFieldName)
		{
			this.conditionFieldName = new string[1];
			this.conditionFieldName[0] = conditionFieldName;
		}

		/// <summary>
		/// Attribute used to show or hide the Field depending on certain conditions
		/// </summary>
		/// <param name="conditionFieldName">Name of the Field to compare (bool, enum, int or float)</param>
		/// <param name="comparationValue">Value to compare</param>
		public ShowWhenAttribute(string conditionFieldName, object comparationValue = null)
		{
			this.conditionFieldName = new string[1];
			this.comparationValueArray = new object[1];
			this.conditionFieldName[0] = conditionFieldName;
			this.comparationValueArray[0] = comparationValue;
		}

		/// <summary>
		/// Attribute used to show or hide the Field depending on certain conditions
		/// </summary>
		/// <param name="conditionFieldName">Name of the Field to compare (bool, enum, int or float)</param>
		/// <param name="comparationValueArray">Array of values to compare (only for enums)</param>
		public ShowWhenAttribute(string conditionFieldName, object[] comparationValueArray = null)
		{
			this.conditionFieldName = new string[1];
			this.conditionFieldName[0] = conditionFieldName;
			this.comparationValueArray = comparationValueArray;
		}


		public ShowWhenAttribute(string[] conditionFieldName, object[] comparationValueArray)
		{
			if (conditionFieldName.Length != comparationValueArray.Length)
			{
				throw new ArgumentException("Both arrays must be same length!");
			}
			this.conditionFieldName = conditionFieldName;
			this.comparationValueArray = comparationValueArray;
		}
	}
}