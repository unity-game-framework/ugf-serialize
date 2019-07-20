using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Serialize.Runtime
{
    public class SerializerProvider : ISerializerProvider
    {
        public int DataTypesCount { get { return m_serializers.Count; } }

        private readonly Dictionary<Type, SerializerCollection> m_serializers = new Dictionary<Type, SerializerCollection>();

        private class SerializerCollection
        {
            public Dictionary<string, ISerializer> Serializers { get; } = new Dictionary<string, ISerializer>();
            public IReadOnlyDictionary<string, ISerializer> AsReadOnly { get; }

            public SerializerCollection()
            {
                AsReadOnly = new ReadOnlyDictionary<string, ISerializer>(Serializers);
            }
        }

        public void Add(string name, ISerializer serializer)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("The specified name is null or empty.", nameof(name));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (serializer.DataType == null) throw new ArgumentException("The data type is not specified in serializer.", nameof(serializer));

            if (!m_serializers.TryGetValue(serializer.DataType, out SerializerCollection serializerCollection))
            {
                serializerCollection = new SerializerCollection();

                m_serializers.Add(serializer.DataType, serializerCollection);
            }

            serializerCollection.Serializers.Add(name, serializer);
        }

        public bool Remove<T>(string name)
        {
            return Remove(typeof(T), name);
        }

        public bool Remove(Type dataType, string name)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("The specified name is null or empty.", nameof(name));

            if (m_serializers.TryGetValue(dataType, out SerializerCollection serializerCollection) && serializerCollection.Serializers.Remove(name))
            {
                if (serializerCollection.Serializers.Count == 0)
                {
                    m_serializers.Remove(dataType);
                }

                return true;
            }

            return false;
        }

        public void Clear()
        {
            m_serializers.Clear();
        }

        public void Clear<T>()
        {
            Clear(typeof(T));
        }

        public void Clear(Type dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            if (m_serializers.TryGetValue(dataType, out SerializerCollection serializerCollection))
            {
                serializerCollection.Serializers.Clear();

                m_serializers.Remove(dataType);
            }
        }

        public ISerializer<T> Get<T>(string name)
        {
            return (ISerializer<T>)Get(typeof(T), name);
        }

        public ISerializer Get(Type dataType, string name)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("The specified name is null or empty.", nameof(name));

            return m_serializers[dataType].Serializers[name];
        }

        public bool TryGet<T>(string name, out ISerializer<T> serializer)
        {
            if (TryGet(typeof(T), name, out ISerializer result) && result is ISerializer<T> cast)
            {
                serializer = cast;
                return true;
            }

            serializer = null;
            return false;
        }

        public bool TryGet(Type dataType, string name, out ISerializer serializer)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("The specified name is null or empty.", nameof(name));

            if (m_serializers.TryGetValue(dataType, out SerializerCollection serializerCollection))
            {
                return serializerCollection.Serializers.TryGetValue(name, out serializer);
            }

            serializer = null;
            return false;
        }

        public IReadOnlyDictionary<string, ISerializer> GetSerializers<T>()
        {
            return GetSerializers(typeof(T));
        }

        public IReadOnlyDictionary<string, ISerializer> GetSerializers(Type dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            return m_serializers[dataType].AsReadOnly;
        }

        public bool TryGetSerializers<T>(out IReadOnlyDictionary<string, ISerializer> serializers)
        {
            return TryGetSerializers(typeof(T), out serializers);
        }

        public bool TryGetSerializers(Type dataType, out IReadOnlyDictionary<string, ISerializer> serializers)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            if (m_serializers.TryGetValue(dataType, out SerializerCollection serializerCollection))
            {
                serializers = serializerCollection.AsReadOnly;
                return true;
            }

            serializers = null;
            return false;
        }

        public string GetName(ISerializer serializer)
        {
            if (TryGetName(serializer, out string name))
            {
                return name;
            }

            throw new ArgumentException($"The name not found for the specified serializer: '{serializer}'.", nameof(serializer));
        }

        public bool TryGetName(ISerializer serializer, out string name)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (serializer.DataType == null) throw new ArgumentException("The data type is not specified in serializer.", nameof(serializer));

            if (m_serializers.TryGetValue(serializer.DataType, out SerializerCollection serializerCollection))
            {
                foreach (KeyValuePair<string, ISerializer> pair in serializerCollection.Serializers)
                {
                    if (pair.Value == serializer)
                    {
                        name = pair.Key;
                        return true;
                    }
                }

                name = null;
                return false;
            }

            name = null;
            return false;
        }

        public IReadOnlyCollection<Type> GetDataTypes()
        {
            return m_serializers.Keys;
        }
    }
}
