using System;
using System.Threading.Tasks;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerAsync<TData> : Serializer<TData>, ISerializerAsync<TData>
    {
        public Task<TData> SerializeAsync<T>(T target)
        {
            return SerializeAsync((object)target);
        }

        public Task<TData> SerializeAsync(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return OnSerializeAsync(target);
        }

        public async Task<T> DeserializeAsync<T>(TData data)
        {
            return (T)await DeserializeAsync(typeof(T), data);
        }

        public Task<object> DeserializeAsync(Type targetType, TData data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            return OnDeserializeAsync(targetType, data);
        }

        async Task<object> ISerializerAsync.SerializeAsync<T>(T target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return await OnSerializeAsync(target);
        }

        async Task<object> ISerializerAsync.SerializeAsync(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return await OnSerializeAsync(target);
        }

        async Task<T> ISerializerAsync.DeserializeAsync<T>(object data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return (T)await OnDeserializeAsync(typeof(T), (TData)data);
        }

        Task<object> ISerializerAsync.DeserializeAsync(Type targetType, object data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            return OnDeserializeAsync(targetType, (TData)data);
        }

        protected abstract Task<TData> OnSerializeAsync(object target);
        protected abstract Task<object> OnDeserializeAsync(Type targetType, TData data);
    }
}
