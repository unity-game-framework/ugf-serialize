using System;
using System.Text;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    /// <summary>
    /// Represents serializer that use <see cref="JsonUtility"/> to serialize a specified target to Json representation and convert to byte array.
    /// </summary>
    public class SerializerUnityJsonBytes : SerializerBase<byte[]>
    {
        /// <summary>
        /// Gets the encoding used to convert Json data to byte array.
        /// </summary>
        public Encoding Encoding { get; }

        /// <summary>
        /// Creates serializer with the specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding used to convert Json data to byte array.</param>
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
