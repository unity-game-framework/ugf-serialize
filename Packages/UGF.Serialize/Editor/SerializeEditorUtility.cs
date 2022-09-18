using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UGF.Serialize.Runtime;
using UnityEditor;

namespace UGF.Serialize.Editor
{
    public static class SerializeEditorUtility
    {
        public static void GetSerializeTypes(IDictionary<object, Type> types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));

            foreach ((Type type, SerializeTypeAttribute attribute) in GetSerializeTypes())
            {
                if (attribute.HasId)
                {
                    types.Add(attribute.Id, type);
                }
            }
        }

        public static IEnumerable<(Type type, SerializeTypeAttribute Attribute)> GetSerializeTypes()
        {
            TypeCache.TypeCollection collection = TypeCache.GetTypesWithAttribute<SerializeTypeAttribute>();

            foreach (Type type in collection)
            {
                var attribute = type.GetCustomAttribute<SerializeTypeAttribute>();

                yield return (type, attribute);
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
