using UnityEditor;
using UnityEngine;

namespace dninosores.UnityAccessors
{
    /// <summary>
    /// Property drawer for implementations of AnyOrConstantAccessor.
    /// This class works for any implementation of AnyOrConstantAccessor, however due to Unity limitations for 
    /// each implementation of AnyOrConstantAccessor you must copy this class and change the type for the 
    /// CustomPropertyDrawer attribute.
    /// </summary>
	[CustomPropertyDrawer(typeof(Vector3OrConstantAccessor))]
	public class Vector3OrConstantDrawer : PropertyDrawer
	{
        private const float BOOL_SPACING = 0f;
        private const float BOOL_PADDING = 2f;
        private const float ACCESSOR_SPACING = 15;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty isAccessed = property.FindPropertyRelative("accessed");
            if (!isAccessed.boolValue)
            {
                return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"), label);
            }
            else
            {
                return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("accessorValue"), label, true);
            }
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            SerializedProperty isAccessed = property.FindPropertyRelative("accessed");
            SerializedProperty constantValue = property.FindPropertyRelative("value");
            SerializedProperty accessorValue = property.FindPropertyRelative("accessorValue");

           

            Rect boolRect = new Rect(rect.x, rect.y, EditorGUI.GetPropertyHeight(isAccessed) + BOOL_SPACING, EditorGUI.GetPropertyHeight(isAccessed));
           // GUI.Button(boolRect, new GUIContent("C"));
            // EditorGUI.DrawRect(boolRect, Color.red);
           isAccessed.boolValue = EditorGUI.Toggle(boolRect, isAccessed.boolValue, GUI.skin.button);


            float spacing = EditorGUI.GetPropertyHeight(isAccessed) + BOOL_SPACING + BOOL_PADDING;
            rect.x += spacing;
            rect.width -= spacing;
            
            if (!isAccessed.boolValue)
            {
                EditorGUI.PropertyField(rect, constantValue, new GUIContent(property.displayName), true);
            }
            else
            {
                rect.x += ACCESSOR_SPACING;
                rect.width -= ACCESSOR_SPACING;
                EditorGUI.PropertyField(rect, accessorValue, new GUIContent(property.displayName + " (" + constantValue.type + ")"), true);
            }

            //EditorGUI.DrawRect(rect, Color.red);
        }
    }
}