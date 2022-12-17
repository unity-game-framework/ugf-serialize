using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Types;
using UnityEditor;
using UnityEngine;

namespace UGF.Serialize.Editor
{
    internal class SerializeTypeDataCollectionDrawer : ReorderableListKeyAndValueDrawer
    {
        private SerializedProperty m_propertyIdType;

        public SerializeTypeDataCollectionDrawer(SerializedProperty serializedProperty) : base(serializedProperty, "m_idValue", "m_type")
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            m_propertyIdType = serializedProperty.FindPropertyRelative("m_idType.m_value");

            base.OnDrawElementContent(position, serializedProperty, index, isActive, isFocused);

            m_propertyIdType = null;
        }

        protected override void OnDrawKey(Rect position, SerializedProperty serializedProperty)
        {
            string typeName = EditorElementsUtility.TextFieldWithDropdown(position, GUIContent.none, serializedProperty.stringValue, OnGetIdTypeItems);

            if (typeName != serializedProperty.stringValue && typeName != m_propertyIdType.stringValue)
            {
                m_propertyIdType.stringValue = typeName;
            }
        }

        private IEnumerable<DropdownItem<string>> OnGetIdTypeItems()
        {
            var items = new List<DropdownItem<string>>();
            var type = Type.GetType(m_propertyIdType.stringValue);

            items.Add(GetTypeItem<byte>(type));
            items.Add(GetTypeItem<short>(type));
            items.Add(GetTypeItem<int>(type));
            items.Add(GetTypeItem<long>(type));
            items.Add(GetTypeItem<string>(type));

            return items;
        }

        private DropdownItem<string> GetTypeItem<T>(Type typeSelected)
        {
            return new DropdownItem<string>(typeof(T).Name, typeof(T).AssemblyQualifiedName);
        }
    }
}
