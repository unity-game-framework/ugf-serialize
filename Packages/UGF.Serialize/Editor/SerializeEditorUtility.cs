using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UGF.Serialize.Runtime;
using UnityEditor;

namespace UGF.Serialize.Editor
{
    public static partial class SerializeEditorUtility
    {
        public static Dictionary<Type, SerializeTypeAttribute> GetSerializeTypeByAttribute()
        {
            var result = new Dictionary<Type, SerializeTypeAttribute>();

            GetSerializeTypeByAttribute(result);

            return result;
        }

        public static void GetSerializeTypeByAttribute(IDictionary<Type, SerializeTypeAttribute> attributes)
        {
            if (attributes == null) throw new ArgumentNullException(nameof(attributes));

            TypeCache.TypeCollection collection = TypeCache.GetTypesWithAttribute<SerializeTypeAttribute>();

            for (int i = 0; i < collection.Count; i++)
            {
                Type type = collection[i];
                var attribute = type.GetCustomAttribute<SerializeTypeAttribute>();

                attributes.Add(type, attribute);
            }
        }

        public static bool IsValidSerializableType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return !type.IsAbstract
                   && !type.IsArray
                   && !type.IsEnum
                   && !type.IsInterface
                   && !type.IsGenericTypeDefinition
                   && !type.IsGenericType
                   && type.GetConstructor(Type.EmptyTypes) != null
                   && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null;
        }
    }
}
