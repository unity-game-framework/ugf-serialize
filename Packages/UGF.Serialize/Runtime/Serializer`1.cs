using System;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime
{
    public abstract class Serializer<TData> : Serializer, ISerializer<TData>
    {
        public override Type DataType { get; } = typeof(TData);

        public new TData Serialize<T>(T target, IContext context)
        {
            return Serialize((object)target, context);
        }

        public new TData Serialize(object target, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return (TData)OnSerialize(target, context);
        }

        public T Deserialize<T>(TData data, IContext context)
        {
            return (T)OnDeserialize(typeof(T), data, context);
        }

        public object Deserialize(Type targetType, TData data, IContext context)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnDeserialize(targetType, data, context);
        }

        protected override object OnDeserialize(Type targetType, object data, IContext context)
        {
            return OnDeserialize(targetType, (TData)data, context);
        }

        protected abstract object OnDeserialize(Type targetType, TData data, IContext context);
    }
}
