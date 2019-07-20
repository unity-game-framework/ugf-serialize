using System;
using System.Text;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    public class SerializerUnityJsonBytes : Serializer<byte[]>
    {
        public Encoding Encoding { get; }

        public SerializerUnityJsonBytes(Encoding encoding)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public override byte[] Serialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            string text = JsonUtility.ToJson(target);

            return Encoding.GetBytes(text);
        }

        public override object Deserialize(Type targetType, byte[] data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            string text = Encoding.GetString(data);

            return JsonUtility.FromJson(text, targetType);
        }
    }
}
