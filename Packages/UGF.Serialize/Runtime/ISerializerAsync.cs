using System;
using System.Threading.Tasks;

namespace UGF.Serialize.Runtime
{
    public interface ISerializerAsync
    {
        Type DataType { get; }

        Task<object> SerializeAsync<T>(T target);
        Task<object> SerializeAsync(object target);
        Task<T> DeserializeAsync<T>(object data);
        Task<object> DeserializeAsync(Type targetType, object data);
    }
}
