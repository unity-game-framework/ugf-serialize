using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Serialize.Runtime
{
    /// <summary>
    /// Represents provider of the serializers.
    /// </summary>
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

        public void Add(string id, ISerializer serializer)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (serializer.DataType == null) throw new ArgumentException("The data type is not specified in serializer.", nameof(serializer));

            if (!m_serializers.TryGetValue(serializer.DataType, out SerializerCollection collection))
            {
                collection = new SerializerCollection();

                m_serializers.Add(serializer.DataType, collection);
            }

            collection.Serializers.Add(id, serializer);
        }

        public bool Remove<T>(string id)
        {
            return Remove(typeof(T), id);
        }

        public bool Remove(Type dataType, string id)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (m_serializers.TryGetValue(dataType, out SerializerCollection collection) && collection.Serializers.Remove(id))
            {
                if (collection.Serializers.Count == 0)
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

            if (m_serializers.TryGetValue(dataType, out SerializerCollection collection))
            {
                collection.Serializers.Clear();

                m_serializers.Remove(dataType);
            }
        }

        public ISerializer<T> Get<T>(string id)
        {
            return (ISerializer<T>)Get(typeof(T), id);
        }

        public ISerializer Get(Type dataType, string id)
        {
            return TryGet(dataType, id, out ISerializer serializer)
                ? serializer
                : throw new ArgumentException($"Serializer not found by the specified data type and id: '{dataType}', '{id}'.", nameof(dataType));
        }

        public bool TryGet<T>(string id, out ISerializer<T> serializer)
        {
            if (TryGet(typeof(T), id, out ISerializer value))
            {
                serializer = (ISerializer<T>)value;
                return true;
            }

            serializer = null;
            return false;
        }

        public bool TryGet(Type dataType, string id, out ISerializer serializer)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (m_serializers.TryGetValue(dataType, out SerializerCollection collection))
            {
                return collection.Serializers.TryGetValue(id, out serializer);
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
            return TryGetSerializers(dataType, out IReadOnlyDictionary<string, ISerializer> serializers)
                ? serializers
                : throw new ArgumentException($"Collection of serializers not found by the specified type of the data: '{dataType}'.", nameof(dataType));
        }

        public bool TryGetSerializers<T>(out IReadOnlyDictionary<string, ISerializer> serializers)
        {
            return TryGetSerializers(typeof(T), out serializers);
        }

        public bool TryGetSerializers(Type dataType, out IReadOnlyDictionary<string, ISerializer> serializers)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            if (m_serializers.TryGetValue(dataType, out SerializerCollection collection))
            {
                serializers = collection.AsReadOnly;
                return true;
            }

            serializers = null;
            return false;
        }

        public string GetId(ISerializer serializer)
        {
            return TryGetId(serializer, out string id) ? id : throw new ArgumentException($"id not found for the specified serializer: '{serializer}'.", nameof(serializer));
        }

        public bool TryGetId(ISerializer serializer, out string id)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (serializer.DataType == null) throw new ArgumentException("The data type is not specified in serializer.", nameof(serializer));

            if (m_serializers.TryGetValue(serializer.DataType, out SerializerCollection collection))
            {
                foreach (KeyValuePair<string, ISerializer> pair in collection.Serializers)
                {
                    if (pair.Value == serializer)
                    {
                        id = pair.Key;
                        return true;
                    }
                }
            }

            id = null;
            return false;
        }

        public IReadOnlyCollection<Type> GetDataTypes()
        {
            return m_serializers.Keys;
        }
    }
}
