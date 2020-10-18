#if UGF_SERIALIZE_JSONNET
using System;
using System.Threading.Tasks;
using UGF.JsonNet.Runtime;
using Unity.Profiling;

namespace UGF.Serialize.Runtime.JsonNet
{
    public class SerializerJsonNet : SerializerAsyncBase<string>
    {
        public bool Readable { get; }

        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerJsonNet()
        {
            m_markerSerialize = new ProfilerMarker("SerializerJsonNet.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerJsonNet.Deserialize");
        }
#endif

        public SerializerJsonNet(bool readable = false)
        {
            Readable = readable;
        }

        public override string Serialize(object target)
        {
            return InternalSerialize(target, Readable);
        }

        public override object Deserialize(Type targetType, string data)
        {
            return InternalDeserialize(targetType, data);
        }

        public override Task<string> SerializeAsync(object target)
        {
            return Task.Run(() => InternalSerialize(target, Readable));
        }

        public override Task<object> DeserializeAsync(Type targetType, string data)
        {
            return Task.Run(() => InternalDeserialize(targetType, data));
        }

        private static string InternalSerialize(object target, bool readable)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            m_markerSerialize.Begin();

            string result = JsonNetUtility.ToJson(target, readable);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            m_markerDeserialize.Begin();

            object target = JsonNetUtility.FromJson(data, targetType);

            m_markerDeserialize.End();

            return target;
        }
    }
}
#endif
