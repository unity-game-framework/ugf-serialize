using System;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    /// <summary>
    /// Represents serializer that use <see cref="JsonUtility"/> to serialize a specified target to Json representation and vice versa.
    /// </summary>
    public class SerializerUnityJson : SerializerBase<string>
    {
        /// <summary>
        /// Gets the value that determines whether to use readable layout of the Json.
        /// </summary>
        public bool Readable { get; }

        /// <summary>
        /// Creates serialize with the specified readable option.
        /// </summary>
        /// <param name="readable">The value that determines whether to use readable layout of the Json.</param>
        public SerializerUnityJson(bool readable)
        {
            Readable = readable;
        }

        public override string Serialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return JsonUtility.ToJson(target, Readable);
        }

        public override object Deserialize(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            return JsonUtility.FromJson(data, targetType);
        }
    }
}
