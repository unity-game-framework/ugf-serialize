using System;
using System.Threading.Tasks;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerAsyncBase<TData> : SerializerBase<TData>, ISerializerAsync<TData>
    {
        public virtual Task<TData> SerializeAsync<T>(T target)
        {
            return SerializeAsync((object)target);
        }

        public abstract Task<TData> SerializeAsync(object target);

        public virtual async Task<T> DeserializeAsync<T>(TData data)
        {
            return (T)await DeserializeAsync(typeof(T), data);
        }

        public abstract Task<object> DeserializeAsync(Type targetType, TData data);

        async Task<object> ISerializerAsync.SerializeAsync<T>(T target)
        {
            return await SerializeAsync(target);
        }

        async Task<object> ISerializerAsync.SerializeAsync(object target)
        {
            return await SerializeAsync(target);
        }

        Task<T> ISerializerAsync.DeserializeAsync<T>(object data)
        {
            return DeserializeAsync<T>((TData)data);
        }

        Task<object> ISerializerAsync.DeserializeAsync(Type targetType, object data)
        {
            return DeserializeAsync(targetType, (TData)data);
        }
    }
}
