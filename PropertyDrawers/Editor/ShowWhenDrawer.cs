//Adapted from https://gist.github.com/deebrol/02f61b7611fd4eca923776077b92dfc2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace dninosores.UnityValueAccessors.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ShowWhenAttribute))]
    public class ShowWhenDrawer : PropertyDrawer
    {
        private bool showField = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowWhenAttribute attribute = (ShowWhenAttribute)this.attribute;


            showField = true;
            for (int i = 0; i < attribute.conditionFieldName.Length; i++)
            {
                SerializedProperty conditionField = property.serializedObject.FindProperty(attribute.conditionFieldName[i]);

                // We check that exist a Field with the parameter name
                if (conditionField == null)
                {
                    ShowError(position, label, "Error getting the condition Field. Check the name.");
                    return;
                }
                showField = showField && ShouldShowField(property, conditionField, attribute.comparationValueArray[i], position, label);
                if (!showField)
                {
                    break;
                }
            }

            if (showField)
                EditorGUI.PropertyField(position, property, true);
        }


        private bool ShouldShowField(SerializedProperty property, SerializedProperty conditionField, object comparationValue, Rect position, GUIContent label)
        {
            switch (conditionField.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    try
                    {
                        bool boolComparationValue = comparationValue == null || (bool)comparationValue;
                        return conditionField.boolValue == boolComparationValue;
                    }
                    catch
                    {
                        ShowError(position, label, "Invalid comparation Value Type");
                        return false;
                    }
                    break;
                case SerializedPropertyType.Enum:
                    object paramEnum = comparationValue;

                    if (paramEnum == null)
                    {
                        ShowError(position, label, "The comparation enum value is null");
                        return false;
                    }
                    else if (IsEnum(paramEnum))
                    {
                        if (!CheckSameEnumType(new[] { paramEnum.GetType() }, property.serializedObject.targetObject.GetType(), conditionField.name))
                        {
                            ShowError(position, label, "Enum Types doesn't match");
                            return false;
                        }
                        else
                        {
                            string enumValue = Enum.GetValues(paramEnum.GetType()).GetValue(conditionField.enumValueIndex).ToString();
                            if (paramEnum.ToString() != enumValue)
                                return false;
                            else
                                return true;
                        }
                    }
                    else
                    {
                        ShowError(position, label, "The comparation enum value is not an enum");
                        return false;
                    }
                    break;
                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Float:
                    string stringValue;
                    bool error = false;

                    float conditionValue = 0;
                    if (conditionField.propertyType == SerializedPropertyType.Integer)
                        conditionValue = conditionField.intValue;
                    else if (conditionField.propertyType == SerializedPropertyType.Float)
                        conditionValue = conditionField.floatValue;

                    try
                    {
                        stringValue = (string)comparationValue;
                    }
                    catch
                    {
                        ShowError(position, label, "Invalid comparation Value Type");
                        return false;
                    }

                    if (stringValue.StartsWith("=="))
                    {
                        float? value = GetValue(stringValue, "==");
                        if (value == null)
                            error = true;
                        else
                            return conditionValue == value;
                    }
                    else if (stringValue.StartsWith("!="))
                    {
                        float? value = GetValue(stringValue, "!=");
                        if (value == null)
                            error = true;
                        else
                            return conditionValue != value;
                    }
                    else if (stringValue.StartsWith("<="))
                    {
                        float? value = GetValue(stringValue, "<=");
                        if (value == null)
                            error = true;
                        else
                            return conditionValue <= value;
                    }
                    else if (stringValue.StartsWith(">="))
                    {
                        float? value = GetValue(stringValue, ">=");
                        if (value == null)
                            error = true;
                        else
                            return conditionValue >= value;
                    }
                    else if (stringValue.StartsWith("<"))
                    {
                        float? value = GetValue(stringValue, "<");
                        if (value == null)
                            error = true;
                        else
                            return conditionValue < value;
                    }
                    else if (stringValue.StartsWith(">"))
                    {
                        float? value = GetValue(stringValue, ">");
                        if (value == null)
                            error = true;
                        else
                            return conditionValue > value;
                    }

                    if (error)
                    {
                        ShowError(position, label, "Invalid comparation instruction for Int or float value");
                        return false;
                    }
                    break;
                default:
                    ShowError(position, label, "This type has not supported.");
                    return false;
            }
            return false;
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (showField)
                return EditorGUI.GetPropertyHeight(property);
            else
                return -EditorGUIUtility.standardVerticalSpacing;
        }

        /// <summary>
        /// Return if the object is enum and not null
        /// </summary>
        private static bool IsEnum(object obj)
        {
            return obj != null && obj.GetType().IsEnum;
        }

        /// <summary>
        /// Return if all the objects are enums and not null
        /// </summary>
        private static bool IsEnum(object[] obj)
        {
            return obj != null && obj.All(o => o.GetType().IsEnum);
        }

        /// <summary>
        /// Check if the field with name "fieldName" has the same class as the "checkTypes" classes through reflection
        /// </summary>
        private static bool CheckSameEnumType(IEnumerable<Type> checkTypes, Type classType, string fieldName)
        {
            FieldInfo memberInfo = classType.GetField(fieldName);

            if (memberInfo != null)
                return checkTypes.All(x => x == memberInfo.FieldType);

            return false;
        }

        private void ShowError(Rect position, GUIContent label, string errorText)
        {
            EditorGUI.LabelField(position, label, new GUIContent(errorText));
            showField = true;
        }

        /// <summary>
        /// Return the float value in the content string removing the remove string
        /// </summary>
        private static float? GetValue(string content, string remove)
        {
            string removed = content.Replace(remove, "");
            try
            {
                return float.Parse(removed);
            }
            catch
            {
                return null;
            }
        }
    }
}