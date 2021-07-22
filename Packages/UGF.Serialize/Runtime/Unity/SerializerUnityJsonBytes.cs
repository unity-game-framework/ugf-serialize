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
    public class SerializerUnityJsonBytes : SerializerAsync<byte[]>
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
        /// Creates serialize with the default encoding.
        /// </summary>
        public SerializerUnityJsonBytes()
        {
            Encoding = Encoding.Default;
        }

        /// <summary>
        /// Creates serializer with the specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding used to convert Json data to byte array.</param>
        public SerializerUnityJsonBytes(Encoding encoding)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        protected override object OnSerialize(object target)
        {
            return InternalSerialize(Encoding, target);
        }

        protected override object OnDeserialize(Type targetType, byte[] data)
        {
            return InternalDeserialize(Encoding, targetType, data);
        }

        protected override Task<byte[]> OnSerializeAsync(object target)
        {
            return Task.Run(() => InternalSerialize(Encoding, target));
        }

        protected override Task<object> OnDeserializeAsync(Type targetType, byte[] data)
        {
            return Task.Run(() => InternalDeserialize(Encoding, targetType, data));
        }

        private static byte[] InternalSerialize(Encoding encoding, object target)
        {
            m_markerSerialize.Begin();

            string text = JsonUtility.ToJson(target);
            byte[] result = encoding.GetBytes(text);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Encoding encoding, Type targetType, byte[] data)
        {
            m_markerDeserialize.Begin();

            string text = data.Length > 0 ? encoding.GetString(data) : "{}";
            object result = JsonUtility.FromJson(text, targetType);

            m_markerDeserialize.End();

            return result;
        }
    }
}
