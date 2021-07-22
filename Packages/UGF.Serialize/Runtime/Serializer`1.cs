using System;

namespace UGF.Serialize.Runtime
{
    public abstract class Serializer<TData> : Serializer, ISerializer<TData>
    {
        public override Type DataType { get; } = typeof(TData);

        public new TData Serialize<T>(T target)
        {
            return (TData)OnSerialize(target);
        }

        public new TData Serialize(object target)
        {
            return (TData)OnSerialize(target);
        }

        public T Deserialize<T>(TData data)
        {
            return (T)OnDeserialize(typeof(T), data);
        }

        public object Deserialize(Type targetType, TData data)
        {
            return OnDeserialize(targetType, data);
        }

        protected override object OnDeserialize(Type targetType, object data)
        {
            return OnDeserialize(targetType, (TData)data);
        }

        protected abstract object OnDeserialize(Type targetType, TData data);
    }
}
