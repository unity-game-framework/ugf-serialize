using System;
using UGF.Serialize.Runtime;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;

namespace UGF.Serialize.Editor.Unity
{
    public class SerializerUnityJsonEditor : Serializer<string>
    {
        public bool Readable { get; }

        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerUnityJsonEditor()
        {
            m_markerSerialize = new ProfilerMarker("SerializerUnityJsonEditor.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerUnityJsonEditor.Deserialize");
        }
#endif

        public SerializerUnityJsonEditor(bool readable = false)
        {
            Readable = readable;
        }

        protected override object OnSerialize(object target)
        {
            return InternalSerialize(target, Readable);
        }

        protected override object OnDeserialize(Type targetType, string data)
        {
            return InternalDeserialize(targetType, data);
        }

        private static string InternalSerialize(object target, bool readable)
        {
            m_markerSerialize.Begin();

            string result = EditorJsonUtility.ToJson(target, readable);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Type targetType, string data)
        {
            m_markerDeserialize.Begin();

            object target = CreateInstance(targetType);

            EditorJsonUtility.FromJsonOverwrite(data, target);

            m_markerDeserialize.End();

            return target;
        }

        private static object CreateInstance(Type targetType)
        {
            return typeof(ScriptableObject).IsAssignableFrom(targetType)
                ? ScriptableObject.CreateInstance(targetType)
                : Activator.CreateInstance(targetType);
        }
    }
}
