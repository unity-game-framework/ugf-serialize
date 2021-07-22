using System;
using System.Text;
using System.Threading.Tasks;
using Unity.Profiling;

namespace UGF.Serialize.Runtime.Bytes
{
    public class SerializerTextToBytes : SerializerAsync<byte[]>
    {
        public Encoding Encoding { get; }
        public ISerializer<string> Serializer { get; }

        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerTextToBytes()
        {
            m_markerSerialize = new ProfilerMarker("SerializerTextToBytes.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerTextToBytes.Deserialize");
        }
#endif

        public SerializerTextToBytes(ISerializer<string> serializer) : this(Encoding.Default, serializer)
        {
        }

        public SerializerTextToBytes(Encoding encoding, ISerializer<string> serializer)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public override byte[] Serialize(object target)
        {
            return InternalSerialize(Encoding, Serializer, target);
        }

        public override object Deserialize(Type targetType, byte[] data)
        {
            return InternalDeserialize(Encoding, Serializer, targetType, data);
        }

        public override Task<byte[]> SerializeAsync(object target)
        {
            return Task.Run(() => InternalSerialize(Encoding, Serializer, target));
        }

        public override Task<object> DeserializeAsync(Type targetType, byte[] data)
        {
            return Task.Run(() => InternalDeserialize(Encoding, Serializer, targetType, data));
        }

        private static byte[] InternalSerialize(Encoding encoding, ISerializer<string> serializer, object target)
        {
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (target == null) throw new ArgumentNullException(nameof(target));

            m_markerSerialize.Begin();

            string text = serializer.Serialize(target);
            byte[] result = encoding.GetBytes(text);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Encoding encoding, ISerializer<string> serializer, Type targetType, byte[] data)
        {
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            m_markerDeserialize.Begin();

            string text = encoding.GetString(data);
            object result = serializer.Deserialize(targetType, text);

            m_markerDeserialize.End();

            return result;
        }
    }
}
