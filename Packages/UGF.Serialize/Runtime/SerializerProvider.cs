using System;
using System.Collections.Generic;

namespace UGF.Serialize.Runtime
{
    public class SerializerProvider : ISerializerProvider
    {
        public ISerializer Default { get { return Serializers[DefaultMode]; } }
        public int DefaultMode { get; set; }
        public Dictionary<int, ISerializer> Serializers { get { return m_serializers; } }

        IDictionary<int, ISerializer> ISerializerProvider.Serializers { get { return m_serializers; } }

        private readonly Dictionary<int, ISerializer> m_serializers = new Dictionary<int, ISerializer>();

        public int GetMode(ISerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            if (TryGetMode(serializer, out int mode))
            {
                return mode;
            }

            throw new ArgumentException($"The mode for the specified serializer not found: '{serializer}'.", nameof(serializer));
        }

        public bool TryGetMode(ISerializer serializer, out int mode)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            foreach (KeyValuePair<int, ISerializer> pair in m_serializers)
            {
                if (pair.Value == serializer)
                {
                    mode = pair.Key;
                    return true;
                }
            }

            mode = default;
            return false;
        }

        public Dictionary<int, ISerializer>.Enumerator GetEnumerator()
        {
            return m_serializers.GetEnumerator();
        }
    }
}
