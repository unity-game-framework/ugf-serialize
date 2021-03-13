using System;
using System.Collections.Generic;

namespace UGF.Serialize.Runtime
{
    /// <summary>
    /// Represents provider of the serializers.
    /// </summary>
    [Obsolete("ISerializerProvider has been deprecated. Use 'IProvider' from 'UGF.RuntimeTools' instead.")]
    public interface ISerializerProvider
    {
        /// <summary>
        /// Gets the count of the total data types.
        /// </summary>
        int DataTypesCount { get; }

        /// <summary>
        /// Adds the specified serialize by the specified id.
        /// </summary>
        /// <param name="id">The id of the serializer.</param>
        /// <param name="serializer">The serializer to add.</param>
        void Add(string id, ISerializer serializer);

        /// <summary>
        /// Removes a serializer by the specified id.
        /// </summary>
        /// <param name="id">The id of the serializer.</param>
        bool Remove<T>(string id);

        /// <summary>
        /// Removes a serializer by the specified id and that support the specified type of the data.
        /// </summary>
        /// <param name="dataType">The type of the data that serializer support.</param>
        /// <param name="id">The id of the serializer.</param>
        bool Remove(Type dataType, string id);

        /// <summary>
        /// Removes all serializers.
        /// </summary>
        void Clear();

        /// <summary>
        /// Removes all serializers that support the specified type of the data.
        /// </summary>
        void Clear<T>();

        /// <summary>
        /// Removes all serializers that support the specified type of the data.
        /// </summary>
        /// <param name="dataType">The type of the data that serializer support.</param>
        void Clear(Type dataType);

        /// <summary>
        /// Gets the serializer by the specified id and that support the specified type of the data.
        /// </summary>
        /// <param name="id">The id of the serializer.</param>
        ISerializer<T> Get<T>(string id);

        /// <summary>
        /// Gets the serializer by the specified id and that support the specified type of the data.
        /// </summary>
        /// <param name="dataType">The type of the data that serializer support.</param>
        /// <param name="id">The id of the serializer.</param>
        ISerializer Get(Type dataType, string id);

        /// <summary>
        /// Tries to get the serializer by the specified id and that support the specified type of the data.
        /// </summary>
        /// <param name="id">The id of the serializer.</param>
        /// <param name="serializer">The found serializer.</param>
        bool TryGet<T>(string id, out ISerializer<T> serializer);

        /// <summary>
        /// Tries to get the serializer by the specified id and that support the specified type of the data.
        /// </summary>
        /// <param name="dataType">The type of the data that serializer support.</param>
        /// <param name="id">The id of the serializer.</param>
        /// <param name="serializer">The found serializer.</param>
        bool TryGet(Type dataType, string id, out ISerializer serializer);

        /// <summary>
        /// Gets the collection of the serializers which support the specified type of the data.
        /// </summary>
        IReadOnlyDictionary<string, ISerializer> GetSerializers<T>();

        /// <summary>
        /// Gets the collection of the serializers which support the specified type of the data.
        /// </summary>
        /// <param name="dataType">The type of the data that serializer support.</param>
        IReadOnlyDictionary<string, ISerializer> GetSerializers(Type dataType);

        /// <summary>
        /// Tries to get the collection of the serializers which support the specified type of the data.
        /// </summary>
        /// <param name="serializers">The found collection of serializers.</param>
        bool TryGetSerializers<T>(out IReadOnlyDictionary<string, ISerializer> serializers);

        /// <summary>
        /// Tries to get the collection of the serializers which support the specified type of the data.
        /// </summary>
        /// <param name="dataType">The type of the data that serializer support.</param>
        /// <param name="serializers">The found collection of serializers.</param>
        bool TryGetSerializers(Type dataType, out IReadOnlyDictionary<string, ISerializer> serializers);

        /// <summary>
        /// Gets the id of the specified serializer.
        /// </summary>
        /// <param name="serializer">The serializer to get name of.</param>
        string GetId(ISerializer serializer);

        /// <summary>
        /// Tries to get the id of the specified serializer.
        /// </summary>
        /// <param name="serializer">The serializer to get id of.</param>
        /// <param name="id">The found id.</param>
        bool TryGetId(ISerializer serializer, out string id);

        /// <summary>
        /// Gets the collection of the all types of the data.
        /// </summary>
        IReadOnlyCollection<Type> GetDataTypes();
    }
}
