using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime
{
    public interface ISerializerAsync<TData> : ISerializerAsync, ISerializer<TData>
    {
        new Task<TData> SerializeAsync<T>(T target, IContext context);
        new Task<TData> SerializeAsync(object target, IContext context);
        Task<T> DeserializeAsync<T>(TData data, IContext context);
        Task<object> DeserializeAsync(Type targetType, TData data, IContext context);
    }
}
