using System;

namespace UGF.Serialize.Runtime
{
    /// <summary>
    /// Represents an abstract base serializer implementation.
    /// </summary>
    public abstract class SerializerBase<TData> : ISerializer<TData>
    {
        public Type DataType { get; } = typeof(TData);

        public virtual TData Serialize<T>(T target)
        {
            return Serialize((object)target);
        }

        public abstract TData Serialize(object target);

        public virtual T Deserialize<T>(TData data)
        {
            return (T)Deserialize(typeof(T), data);
        }

        public abstract object Deserialize(Type targetType, TData data);

        object ISerializer.Serialize<T>(T target)
        {
            return Serialize(target);
        }

        object ISerializer.Serialize(object target)
        {
            return Serialize(target);
        }

        T ISerializer.Deserialize<T>(object data)
        {
            return Deserialize<T>((TData)data);
        }

        object ISerializer.Deserialize(Type targetType, object data)
        {
            return Deserialize(targetType, (TData)data);
        }
    }
}
