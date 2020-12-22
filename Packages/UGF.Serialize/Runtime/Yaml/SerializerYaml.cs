#if UGF_SERIALIZE_YAML
using System;
using System.Threading.Tasks;
using UGF.Yaml.Runtime;
using Unity.Profiling;
using IYamlSerializer = YamlDotNet.Serialization.ISerializer;
using IYamlDeserializer = YamlDotNet.Serialization.IDeserializer;

namespace UGF.Serialize.Runtime.Yaml
{
    public class SerializerYaml : SerializerAsyncBase<string>
    {
        public IYamlSerializer Serializer { get; }
        public IYamlDeserializer Deserializer { get; }

        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerYaml()
        {
            m_markerSerialize = new ProfilerMarker("SerializerYaml.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerYaml.Deserialize");
        }
#endif

        public SerializerYaml() : this(YamlUtility.DefaultSerializer, YamlUtility.DefaultDeserializer)
        {
        }

        public SerializerYaml(IYamlSerializer serializer, IYamlDeserializer deserializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            Deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }

        public override string Serialize(object target)
        {
            return InternalSerialize(target);
        }

        public override object Deserialize(Type targetType, string data)
        {
            return InternalDeserialize(targetType, data);
        }

        public override Task<string> SerializeAsync(object target)
        {
            return Task.Run(() => InternalSerialize(target));
        }

        public override Task<object> DeserializeAsync(Type targetType, string data)
        {
            return Task.Run(() => InternalDeserialize(targetType, data));
        }

        protected virtual string OnSerialize(object target)
        {
            return Serializer.Serialize(target);
        }

        protected virtual object OnDeserialize(Type targetType, string data)
        {
            return Deserializer.Deserialize(data, targetType);
        }

        private string InternalSerialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            m_markerSerialize.Begin();

            string result = OnSerialize(target);

            m_markerSerialize.End();

            return result;
        }

        private object InternalDeserialize(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            m_markerDeserialize.Begin();

            object target = OnDeserialize(targetType, data);

            m_markerDeserialize.End();

            return target;
        }
    }
}
#endif
