using System;
using System.Reflection;
using UnityEngine;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public class ReflectedValueAccessor<T> : ValueAccessor<T>
	{
		public UnityEngine.Object sourceObject;
		public string field;

		public override T GetValue()
		{
			string[] pathArr = field.Split('.');
			object current = sourceObject;
			for (int i = 0; i < pathArr.Length - 1; i++)
			{
				current = GetValueFromObject(current, pathArr[i]);
			}
			string last = pathArr[pathArr.Length - 1];
			return (T)GetValueFromObject(current, last);

		}

		public override void SetValue(T value)
		{
			string[] pathArr = field.Split('.');
			object[] objects = new object[pathArr.Length + 1];
			objects[0] = sourceObject;
			for (int i = 0; i < pathArr.Length; i++)
			{
				string info = pathArr[i];
				objects[i + 1] = GetValueFromObject(objects[i], info);
			}
			objects[objects.Length - 1] = value;
			for (int i = objects.Length - 2; i >= 0; i--)
			{
				SetValueInObject(objects[i], pathArr[i], objects[i + 1]);
			}
		}


		public static object GetValueFromObject(object source, string fieldName)
		{
			Type objType = source.GetType();

			FieldInfo field = objType.GetField(fieldName);
			if (field != null)
			{
				return field.GetValue(source);
			}

			PropertyInfo prop = objType.GetProperty(fieldName);
			if (prop != null)
			{
				return prop.GetValue(source);
			}

			throw new ArgumentException("No field or property with name '" + fieldName + "' on object '" + objType + "' found!");
		}

		public static void SetValueInObject(object source, string fieldName, object value)
		{
			Type objType = source.GetType();
			FieldInfo field = objType.GetField(fieldName);
			if (field != null)
			{
				field.SetValue(source, value);
				return;
			}

			PropertyInfo prop = objType.GetProperty(fieldName);
			if (prop != null)
			{
				prop.SetValue(source, value);
				return;
			}

			throw new ArgumentException("No field or property with name '" + fieldName + "' on object '" + objType + "' found!");
		}

		public override void Reset(GameObject attachedObject)
		{
			sourceObject = attachedObject.GetComponent<Component>();
		}
	}
}