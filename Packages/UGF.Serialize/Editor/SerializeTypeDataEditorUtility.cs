using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Serialize.Runtime;
using UnityEditor;

namespace UGF.Serialize.Editor
{
    public static class SerializeTypeDataEditorUtility
    {
        public static void Refresh(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            Dictionary<Type, SerializeTypeAttribute> types = SerializeEditorUtility.GetSerializeTypeByAttribute();
            var typesCollected = new HashSet<Type>();

            for (int i = 0; i < serializedProperty.arraySize; i++)
            {
                SerializedProperty propertyElement = serializedProperty.GetArrayElementAtIndex(i);

                if (TryUpdate(propertyElement, out _, out Type type) || TryGetTypeData(propertyElement, out _, out type))
                {
                    typesCollected.Add(type);
                }
            }

            foreach ((Type type, SerializeTypeAttribute attribute) in types)
            {
                if (!typesCollected.Contains(type))
                {
                    serializedProperty.InsertArrayElementAtIndex(serializedProperty.arraySize);

                    SerializedProperty propertyElement = serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1);

                    SetTypeData(propertyElement, type, attribute);
                    TryUpdate(propertyElement);
                }
            }
        }

        public static bool TryUpdate(SerializedProperty serializedProperty)
        {
            return TryUpdate(serializedProperty, out _, out _);
        }

        public static bool TryUpdate(SerializedProperty serializedProperty, out object id, out Type type)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyIdValue = serializedProperty.FindPropertyRelative("m_idValue");

            if (string.IsNullOrEmpty(propertyIdValue.stringValue))
            {
                SerializedProperty propertyTypeValue = serializedProperty.FindPropertyRelative("m_type.m_value");

                type = Type.GetType(propertyTypeValue.stringValue);

                var attribute = type?.GetCustomAttribute<SerializeTypeAttribute>();

                if (type != null && attribute != null)
                {
                    SetTypeData(serializedProperty, type, attribute, out id);
                    return true;
                }
            }

            id = default;
            type = default;
            return false;
        }

        public static bool TryGetTypeData(SerializedProperty serializedProperty, out object id, out Type type)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            id = default;
            type = default;

            SerializedProperty propertyIdValue = serializedProperty.FindPropertyRelative("m_idValue");
            SerializedProperty propertyIdType = serializedProperty.FindPropertyRelative("m_idType.m_value");
            SerializedProperty propertyTypeValue = serializedProperty.FindPropertyRelative("m_type.m_value");

            if (!string.IsNullOrEmpty(propertyIdValue.stringValue) && !string.IsNullOrEmpty(propertyIdType.stringValue))
            {
                var idType = Type.GetType(propertyIdType.stringValue);

                id = idType != null ? Convert.ChangeType(propertyIdValue.stringValue, idType) : default;
            }

            if (!string.IsNullOrEmpty(propertyTypeValue.stringValue))
            {
                type = Type.GetType(propertyTypeValue.stringValue);
            }

            return id != null && type != null;
        }

        public static void SetTypeData(SerializedProperty serializedProperty, Type type, SerializeTypeAttribute attribute)
        {
            SetTypeData(serializedProperty, type, attribute, out _);
        }

        public static void SetTypeData(SerializedProperty serializedProperty, Type type, SerializeTypeAttribute attribute, out object id)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));

            id = attribute.HasId ? attribute.Id.ToString() : Guid.NewGuid().ToString("N");

            SetTypeData(serializedProperty, type, id);
        }

        public static void SetTypeData(SerializedProperty serializedProperty, Type type, object id)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (id == null) throw new ArgumentNullException(nameof(id));

            SerializedProperty propertyIdValue = serializedProperty.FindPropertyRelative("m_idValue");
            SerializedProperty propertyIdType = serializedProperty.FindPropertyRelative("m_idType.m_value");
            SerializedProperty propertyTypeValue = serializedProperty.FindPropertyRelative("m_type.m_value");

            propertyIdValue.stringValue = id.ToString();
            propertyIdType.stringValue = id.GetType().AssemblyQualifiedName;
            propertyTypeValue.stringValue = type.AssemblyQualifiedName;
        }
    }
}
