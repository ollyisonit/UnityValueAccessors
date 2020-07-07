using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dninosores.UnityValueAccessors
{
	[Serializable]
	public class ReflectedFloatValueAccessor : ValueAccessor<float>
	{
		public UnityEngine.Object sourceObject;
		public string path;

		public override float GetValue()
		{
			string[] pathArr = path.Split('.');
			object current = sourceObject;
			for (int i = 0; i < pathArr.Length - 1; i++)
			{
				current = GetValueFromObject(current, pathArr[i]);
			}
			string last = pathArr[pathArr.Length - 1];
			return (float) GetValueFromObject(current, last);

		}

		public override void SetValue(float value)
		{
			string[] pathArr = path.Split('.');
			object[] objects = new object[pathArr.Length + 1];
			objects[0] = sourceObject;
			for (int i = 0; i < pathArr.Length; i++)
			{
				string info = pathArr[i];
				objects[i+1] = GetValueFromObject(objects[i], info);
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
	}
}
