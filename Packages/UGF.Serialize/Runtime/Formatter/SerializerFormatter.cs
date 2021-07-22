using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Unity.Profiling;

namespace UGF.Serialize.Runtime.Formatter
{
    /// <summary>
    /// Represents serialized that use formatter to serialize a specified target to byte array and vice versa.
    /// </summary>
    public class SerializerFormatter : SerializerAsync<byte[]>
    {
        /// <summary>
        /// Gets the formatter used to convert target to byte array and vice versa.
        /// </summary>
        public IFormatter Formatter { get; }

        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerFormatter()
        {
            m_markerSerialize = new ProfilerMarker("SerializerFormatter.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerFormatter.Deserialize");
        }
#endif

        /// <summary>
        /// Creates serializer with the BinaryFormatter.
        /// </summary>
        public SerializerFormatter()
        {
            Formatter = new BinaryFormatter();
        }

        /// <summary>
        /// Creates serializer with the specified formatter.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        public SerializerFormatter(IFormatter formatter)
        {
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        protected override object OnSerialize(object target)
        {
            return InternalSerialize(Formatter, target);
        }

        protected override object OnDeserialize(Type targetType, byte[] data)
        {
            return InternalDeserialize(Formatter, data);
        }

        protected override Task<byte[]> OnSerializeAsync(object target)
        {
            return Task.Run(() => InternalSerialize(Formatter, target));
        }

        protected override Task<object> OnDeserializeAsync(Type targetType, byte[] data)
        {
            return Task.Run(() => InternalDeserialize(Formatter, data));
        }

        private static byte[] InternalSerialize(IFormatter formatter, object target)
        {
            m_markerSerialize.Begin();

            using var stream = new MemoryStream();

            formatter.Serialize(stream, target);

            byte[] result = stream.ToArray();

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(IFormatter formatter, byte[] data)
        {
            m_markerDeserialize.Begin();

            using var stream = new MemoryStream(data);
            object result = formatter.Deserialize(stream);

            m_markerDeserialize.End();

            return result;
        }
    }
}
