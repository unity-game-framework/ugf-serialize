using System;
using UGF.Serialize.Runtime;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;

namespace UGF.Serialize.Editor.Unity
{
    public class SerializerUnityJsonEditor : SerializerBase<string>
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

        public override string Serialize(object target)
        {
            return InternalSerialize(target, Readable);
        }

        public override object Deserialize(Type targetType, string data)
        {
            return InternalDeserialize(targetType, data);
        }

        private static string InternalSerialize(object target, bool readable)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            m_markerSerialize.Begin();

            string result = EditorJsonUtility.ToJson(target, readable);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            m_markerDeserialize.Begin();

            object target = CreateInstance(targetType);

            EditorJsonUtility.FromJsonOverwrite(data, target);

            m_markerDeserialize.End();

            return target;
        }

        private static object CreateInstance(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return typeof(ScriptableObject).IsAssignableFrom(targetType)
                ? ScriptableObject.CreateInstance(targetType)
                : Activator.CreateInstance(targetType);
        }
    }
}
