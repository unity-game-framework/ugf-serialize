using System;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    public class SerializerUnityJson : Serializer<string>
    {
        public bool Readable { get; }

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
