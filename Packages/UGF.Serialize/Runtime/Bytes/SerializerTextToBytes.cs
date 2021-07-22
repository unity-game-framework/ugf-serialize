using System;
using System.Text;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
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

        protected override object OnSerialize(object target, IContext context)
        {
            return InternalSerialize(Encoding, Serializer, target, context);
        }

        protected override object OnDeserialize(Type targetType, byte[] data, IContext context)
        {
            return InternalDeserialize(Encoding, Serializer, targetType, data, context);
        }

        protected override Task<byte[]> OnSerializeAsync(object target, IContext context)
        {
            return Task.Run(() => InternalSerialize(Encoding, Serializer, target, context));
        }

        protected override Task<object> OnDeserializeAsync(Type targetType, byte[] data, IContext context)
        {
            return Task.Run(() => InternalDeserialize(Encoding, Serializer, targetType, data, context));
        }

        private static byte[] InternalSerialize(Encoding encoding, ISerializer<string> serializer, object target, IContext context)
        {
            m_markerSerialize.Begin();

            string text = serializer.Serialize(target, context);
            byte[] result = encoding.GetBytes(text);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Encoding encoding, ISerializer<string> serializer, Type targetType, byte[] data, IContext context)
        {
            m_markerDeserialize.Begin();

            string text = encoding.GetString(data);
            object result = serializer.Deserialize(targetType, text, context);

            m_markerDeserialize.End();

            return result;
        }
    }
}
