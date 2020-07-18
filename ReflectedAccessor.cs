using System;
using System.Reflection;
using UnityEngine;

namespace dninosores.UnityAccessors
{
	/// <summary>
	/// Accesses the a field of an object by name.
	/// </summary>
	[Serializable]
	public class ReflectedAccessor<T> : Accessor<T>
	{
		[Tooltip("Object to access a field or property from")]
		public UnityEngine.Object sourceObject;
		[Tooltip("The name of the field to access. A '.' can be used to specify a path. For example, in order to access" +
			"the X position of a transform you would reference a Transform as the sourceObject and write 'position.x' here.")]
		public string field;

		protected override T GetValue()
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

		protected override void SetValue(T value)
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


		/// <summary>
		/// Gets the value from a field or property of an object by name.
		/// </summary>
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

		/// <summary>
		/// Sets the value from a field or property of an object by name.
		/// </summary>
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