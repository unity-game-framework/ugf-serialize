#if UGF_SERIALIZE_JSONNET
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Serialize.Editor.JsonNet
{
    internal class SerializerJsonNetConvertNamesListDrawer : ReorderableListDrawer
    {
        public SerializerJsonNetConvertNamesListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyFrom = serializedProperty.FindPropertyRelative("m_from");
            SerializedProperty propertyTo = serializedProperty.FindPropertyRelative("m_to");

            float space = EditorGUIUtility.standardVerticalSpacing * 2F;
            float labelWidth = EditorGUIUtility.labelWidth + EditorIMGUIUtility.IndentPerLevel;

            var rectFrom = new Rect(position.x, position.y, labelWidth, position.height);
            var rectTo = new Rect(rectFrom.xMax + space, position.y, position.width - rectFrom.width - space, position.height);

            using (new LabelWidthScope(35F))
            {
                EditorGUI.PropertyField(rectFrom, propertyFrom);
            }

            using (new LabelWidthScope(20F))
            {
                EditorGUI.PropertyField(rectTo, propertyTo);
            }
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return false;
        }
    }
}
#endif
