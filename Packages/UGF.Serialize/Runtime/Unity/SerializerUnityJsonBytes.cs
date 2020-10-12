using System;
using System.Text;
using System.Threading.Tasks;
using Unity.Profiling;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    /// <summary>
    /// Represents serializer that use <see cref="JsonUtility"/> to serialize a specified target to Json representation and convert to byte array.
    /// </summary>
    public class SerializerUnityJsonBytes : SerializerAsyncBase<byte[]>
    {
        /// <summary>
        /// Gets the encoding used to convert Json data to byte array.
        /// </summary>
        public Encoding Encoding { get; }

        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerUnityJsonBytes()
        {
            m_markerSerialize = new ProfilerMarker("SerializerUnityJsonBytes.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerUnityJsonBytes.Deserialize");
        }
#endif

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
            return InternalSerialize(Encoding, target);
        }

        public override object Deserialize(Type targetType, byte[] data)
        {
            return InternalDeserialize(Encoding, targetType, data);
        }

        public override Task<byte[]> SerializeAsync(object target)
        {
            return Task.Run(() => InternalSerialize(Encoding, target));
        }

        public override Task<object> DeserializeAsync(Type targetType, byte[] data)
        {
            return Task.Run(() => InternalDeserialize(Encoding, targetType, data));
        }

        private static byte[] InternalSerialize(Encoding encoding, object target)
        {
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));
            if (target == null) throw new ArgumentNullException(nameof(target));

            m_markerSerialize.Begin();

            string text = JsonUtility.ToJson(target);
            byte[] result = encoding.GetBytes(text);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Encoding encoding, Type targetType, byte[] data)
        {
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            m_markerDeserialize.Begin();

            string text = encoding.GetString(data);
            object result = JsonUtility.FromJson(text, targetType);

            m_markerDeserialize.End();

            return result;
        }
    }
}
