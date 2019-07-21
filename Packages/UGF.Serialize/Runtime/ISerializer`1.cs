using System;

namespace UGF.Serialize.Runtime
{
    /// <summary>
    /// Represents a serializer to the specific type of data.
    /// </summary>
    public interface ISerializer<TData> : ISerializer
    {
        /// <summary>
        /// Serializes the specified target to data.
        /// </summary>
        /// <param name="target">The target to serialize.</param>
        new TData Serialize<T>(T target);

        /// <summary>
        /// Serializes the specified target to data.
        /// </summary>
        /// <param name="target">The target to serialize.</param>
        new TData Serialize(object target);

        /// <summary>
        /// Deserializes the specified data to new created object of the specified type.
        /// </summary>
        /// <param name="data">The data used to deserialize new object.</param>
        T Deserialize<T>(TData data);

        /// <summary>
        /// Deserializes the specified data to new created object of the specified type.
        /// </summary>
        /// <param name="targetType">The data used to deserialize new object.</param>
        /// <param name="data">The type of the new created object.</param>
        object Deserialize(Type targetType, TData data);
    }
}
