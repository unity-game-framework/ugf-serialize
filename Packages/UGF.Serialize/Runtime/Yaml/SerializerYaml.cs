#if UGF_SERIALIZE_YAML
using System;
using System.Threading.Tasks;
using UGF.Yaml.Runtime;
using Unity.Profiling;

namespace UGF.Serialize.Runtime.Yaml
{
    public class SerializerYaml : SerializerAsyncBase<string>
    {
        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerYaml()
        {
            m_markerSerialize = new ProfilerMarker("SerializerYaml.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerYaml.Deserialize");
        }
#endif

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

        private static string InternalSerialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            m_markerSerialize.Begin();

            string result = YamlUtility.ToYaml(target);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            m_markerDeserialize.Begin();

            object target = YamlUtility.FromYaml(data, targetType);

            m_markerDeserialize.End();

            return target;
        }
    }
}
#endif
