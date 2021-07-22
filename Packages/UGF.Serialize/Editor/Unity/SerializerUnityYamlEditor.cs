using System;
using UGF.EditorTools.Editor.Yaml;
using UGF.Serialize.Runtime;
using Unity.Profiling;
using Object = UnityEngine.Object;

namespace UGF.Serialize.Editor.Unity
{
    public class SerializerUnityYamlEditor : Serializer<string>
    {
        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerUnityYamlEditor()
        {
            m_markerSerialize = new ProfilerMarker("SerializerUnityYamlEditor.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerUnityYamlEditor.Deserialize");
        }
#endif

        protected override object OnSerialize(object target)
        {
            return InternalSerialize(target);
        }

        protected override object OnDeserialize(Type targetType, string data)
        {
            return InternalDeserialize(data);
        }

        private static string InternalSerialize(object target)
        {
            if (!(target is Object unityTarget)) throw new ArgumentException("Target must be a Unity object.", nameof(target));

            m_markerSerialize.Begin();

            string result = EditorYamlUtility.ToYaml(unityTarget);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(string data)
        {
            m_markerDeserialize.Begin();

            object target = EditorYamlUtility.FromYaml(data);

            m_markerDeserialize.End();

            return target;
        }
    }
}
