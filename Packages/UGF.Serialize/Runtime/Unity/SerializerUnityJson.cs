using System;
using System.Text;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    public class SerializerUnityJson : SerializerBase, ISerializer<string>
    {
        public Encoding Encoding { get; }
        public bool Readable { get; }

        public static ISerializer DefaultCompact { get; } = new SerializerUnityJson(Encoding.Default, false);
        public static ISerializer DefaultReadable { get; } = new SerializerUnityJson(Encoding.Default, true);
        public static int DefaultModeCompact { get; } = 20;
        public static int DefaultModeReadable { get; } = 21;

        public SerializerUnityJson(Encoding encoding, bool readable)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            Readable = readable;
        }

        public override byte[] Serialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            string text = SerializeToJson(target);

            return Encoding.GetBytes(text);
        }

        public override object Deserialize(Type targetType, byte[] data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            string text = Encoding.GetString(data);

            return DeserializeFromJson(targetType, text);
        }

        public string SerializeToJson(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return JsonUtility.ToJson(target, Readable);
        }

        public object DeserializeFromJson(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            return JsonUtility.FromJson(data, targetType);
        }

        string ISerializer<string>.Serialize<T>(T target)
        {
            return SerializeToJson(target);
        }

        string ISerializer<string>.Serialize(object target)
        {
            return SerializeToJson(target);
        }

        T ISerializer<string>.Deserialize<T>(string data)
        {
            return (T)DeserializeFromJson(typeof(T), data);
        }

        object ISerializer<string>.Deserialize(Type targetType, string data)
        {
            return DeserializeFromJson(targetType, data);
        }
    }
}
