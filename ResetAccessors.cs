using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Resets all accessors stored as fields on given object using given GameObject.
	/// </summary>
	public static class ResetAccessors
	{
		/// <summary>
		/// Resets all accessors stored in fields of source to point at components of GameObject attached.
		/// </summary>
		public static void Reset(object source, MonoBehaviour attached, BindingFlags flags = BindingFlags.GetField | BindingFlags.Public | BindingFlags.Instance)
		{
			foreach (FieldInfo field in source.GetType().GetFields(flags))
			{
				Type currentType = field.FieldType;
				bool isAccessor = false;
				while (!isAccessor && currentType != null)
				{
					if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(Accessor<>))
					{
						isAccessor = true;
						break;
					}
					currentType = currentType.BaseType;
				}

				if (isAccessor)
				{

					object fieldObject = field.GetValue(source);
					if (fieldObject == null)
					{
						fieldObject = Activator.CreateInstance(field.FieldType);
						field.SetValue(source, fieldObject);
					}
					MethodInfo resetMethod = fieldObject.GetType().GetMethod("Reset", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
					resetMethod.Invoke(fieldObject, new object[] { attached });

				}
			}
		}
	}
}
