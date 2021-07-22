using System;

namespace UGF.Serialize.Runtime
{
    public abstract class Serializer : ISerializer
    {
        public abstract Type DataType { get; }

        public object Serialize<T>(T target)
        {
            return Serialize((object)target);
        }

        public object Serialize(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return OnSerialize(target);
        }

        public T Deserialize<T>(object data)
        {
            return (T)Deserialize(typeof(T), data);
        }

        public object Deserialize(Type targetType, object data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            return OnDeserialize(targetType, data);
        }

        protected abstract object OnSerialize(object target);
        protected abstract object OnDeserialize(Type targetType, object data);
    }
}
