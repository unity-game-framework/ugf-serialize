using System;
using System.Threading.Tasks;
using Unity.Profiling;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    /// <summary>
    /// Represents serializer that use <see cref="JsonUtility"/> to serialize a specified target to Json representation and vice versa.
    /// </summary>
    public class SerializerUnityJson : SerializerAsync<string>
    {
        /// <summary>
        /// Gets the value that determines whether to use readable layout of the Json.
        /// </summary>
        public bool Readable { get; }

        private static ProfilerMarker m_markerSerialize;
        private static ProfilerMarker m_markerDeserialize;

#if ENABLE_PROFILER
        static SerializerUnityJson()
        {
            m_markerSerialize = new ProfilerMarker("SerializerUnityJson.Serialize");
            m_markerDeserialize = new ProfilerMarker("SerializerUnityJson.Deserialize");
        }
#endif

        /// <summary>
        /// Creates serialize with the specified readable option.
        /// </summary>
        /// <param name="readable">The value that determines whether to use readable layout of the Json.</param>
        public SerializerUnityJson(bool readable = false)
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

        protected override Task<string> OnSerializeAsync(object target)
        {
            return Task.Run(() => InternalSerialize(target, Readable));
        }

        protected override Task<object> OnDeserializeAsync(Type targetType, string data)
        {
            return Task.Run(() => InternalDeserialize(targetType, data));
        }

        private static string InternalSerialize(object target, bool readable)
        {
            m_markerSerialize.Begin();

            string result = JsonUtility.ToJson(target, readable);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Type targetType, string data)
        {
            if (data == string.Empty) data = "{}";

            m_markerDeserialize.Begin();

            object target = JsonUtility.FromJson(data, targetType);

            m_markerDeserialize.End();

            return target;
        }
    }
}
