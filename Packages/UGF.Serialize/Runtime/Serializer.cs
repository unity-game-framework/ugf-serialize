using System;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime
{
    public abstract class Serializer : ISerializer
    {
        public abstract Type DataType { get; }

        public object Serialize<T>(T target, IContext context)
        {
            return Serialize((object)target, context);
        }

        public object Serialize(object target, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnSerialize(target, context);
        }

        public T Deserialize<T>(object data, IContext context)
        {
            return (T)Deserialize(typeof(T), data, context);
        }

        public object Deserialize(Type targetType, object data, IContext context)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnDeserialize(targetType, data, context);
        }

        protected abstract object OnSerialize(object target, IContext context);
        protected abstract object OnDeserialize(Type targetType, object data, IContext context);
    }
}
