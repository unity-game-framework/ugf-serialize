using System;
using System.Threading.Tasks;
using Unity.Profiling;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    /// <summary>
    /// Represents serializer that use <see cref="JsonUtility"/> to serialize a specified target to Json representation and vice versa.
    /// </summary>
    public class SerializerUnityJson : SerializerAsyncBase<string>
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
        public SerializerUnityJson(bool readable)
        {
            Readable = readable;
        }

        public override string Serialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return InternalSerialize(target, Readable);
        }

        public override object Deserialize(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            return InternalDeserialize(targetType, data);
        }

        public override Task<string> SerializeAsync(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return Task.Run(() => InternalSerialize(target, Readable));
        }

        public override Task<object> DeserializeAsync(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            return Task.Run(() => InternalDeserialize(targetType, data));
        }

        private static string InternalSerialize(object target, bool readable)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            m_markerSerialize.Begin();

            string result = JsonUtility.ToJson(target, readable);

            m_markerSerialize.End();

            return result;
        }

        private static object InternalDeserialize(Type targetType, string data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            m_markerDeserialize.Begin();

            object target = JsonUtility.FromJson(data, targetType);

            m_markerDeserialize.End();

            return target;
        }
    }
}
