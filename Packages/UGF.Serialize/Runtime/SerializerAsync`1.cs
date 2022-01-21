using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerAsync<TData> : Serializer<TData>, ISerializerAsync<TData>
    {
        public Task<TData> SerializeAsync<T>(T target, IContext context)
        {
            return SerializeAsync((object)target, context);
        }

        public Task<TData> SerializeAsync(object target, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnSerializeAsync(target, context);
        }

        public async Task<T> DeserializeAsync<T>(TData data, IContext context)
        {
            return (T)await DeserializeAsync(typeof(T), data, context);
        }

        public Task<object> DeserializeAsync(Type targetType, TData data, IContext context)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnDeserializeAsync(targetType, data, context);
        }

        async Task<object> ISerializerAsync.SerializeAsync<T>(T target, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return await OnSerializeAsync(target, context);
        }

        async Task<object> ISerializerAsync.SerializeAsync(object target, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return await OnSerializeAsync(target, context);
        }

        async Task<T> ISerializerAsync.DeserializeAsync<T>(object data, IContext context)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return (T)await OnDeserializeAsync(typeof(T), (TData)data, context);
        }

        Task<object> ISerializerAsync.DeserializeAsync(Type targetType, object data, IContext context)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnDeserializeAsync(targetType, (TData)data, context);
        }

        protected abstract Task<TData> OnSerializeAsync(object target, IContext context);
        protected abstract Task<object> OnDeserializeAsync(Type targetType, TData data, IContext context);
    }
}
