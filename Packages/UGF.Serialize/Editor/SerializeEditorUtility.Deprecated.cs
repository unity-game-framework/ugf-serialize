using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Serialize.Runtime;
using UnityEditor;

namespace UGF.Serialize.Editor
{
    public static partial class SerializeEditorUtility
    {
        [Obsolete("Method 'GetSerializeTypes()' has been deprecated. Use 'GetSerializeTypeByAttribute()' method instead.")]
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

        [Obsolete("Method 'GetSerializeTypes()' has been deprecated. Use 'GetSerializeTypeByAttribute()' method instead.")]
        public static IEnumerable<(Type type, SerializeTypeAttribute Attribute)> GetSerializeTypes()
        {
            TypeCache.TypeCollection collection = TypeCache.GetTypesWithAttribute<SerializeTypeAttribute>();

            foreach (Type type in collection)
            {
                var attribute = type.GetCustomAttribute<SerializeTypeAttribute>();

                yield return (type, attribute);
            }
        }
    }
}
