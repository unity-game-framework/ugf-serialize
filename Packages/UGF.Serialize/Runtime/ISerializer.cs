using System;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime
{
    /// <summary>
    /// Represents a serializer to the specific type of data.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        Type DataType { get; }

        /// <summary>
        /// Serializes the specified target to data.
        /// </summary>
        /// <param name="target">The target to serialize.</param>
        /// <param name="context"></param>
        object Serialize<T>(T target, IContext context);

        /// <summary>
        /// Serializes the specified target to data.
        /// </summary>
        /// <param name="target">The target to serialize.</param>
        /// <param name="context"></param>
        object Serialize(object target, IContext context);

        /// <summary>
        /// Deserializes the specified data to new created object of the specified type.
        /// </summary>
        /// <param name="data">The data used to deserialize new object.</param>
        /// <param name="context"></param>
        T Deserialize<T>(object data, IContext context);

        /// <summary>
        /// Deserializes the specified data to new created object of the specified type.
        /// </summary>
        /// <param name="targetType">The data used to deserialize new object.</param>
        /// <param name="data">The type of the new created object.</param>
        /// <param name="context"></param>
        object Deserialize(Type targetType, object data, IContext context);
    }
}
