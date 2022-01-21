using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerAsync : Serializer, ISerializerAsync
    {
        public override Type DataType { get; } = typeof(object);

        public Task<object> SerializeAsync<T>(T target, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnSerializeAsync(target, context);
        }

        public Task<object> SerializeAsync(object target, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnSerializeAsync(target, context);
        }

        public async Task<T> DeserializeAsync<T>(object data, IContext context)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return (T)await OnDeserializeAsync(typeof(T), data, context);
        }

        public Task<object> DeserializeAsync(Type targetType, object data, IContext context)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnDeserializeAsync(targetType, data, context);
        }

        protected abstract Task<object> OnSerializeAsync(object target, IContext context);
        protected abstract Task<object> OnDeserializeAsync(Type targetType, object data, IContext context);
    }
}
