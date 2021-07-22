using System;
using System.Threading.Tasks;

namespace UGF.Serialize.Runtime
{
    public interface ISerializerAsync<TData> : ISerializerAsync, ISerializer<TData>
    {
        new Task<TData> SerializeAsync<T>(T target);
        new Task<TData> SerializeAsync(object target);
        Task<T> DeserializeAsync<T>(TData data);
        Task<object> DeserializeAsync(Type targetType, TData data);
    }
}
