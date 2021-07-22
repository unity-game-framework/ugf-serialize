using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime
{
    public interface ISerializerAsync : ISerializer
    {
        Task<object> SerializeAsync<T>(T target, IContext context);
        Task<object> SerializeAsync(object target, IContext context);
        Task<T> DeserializeAsync<T>(object data, IContext context);
        Task<object> DeserializeAsync(Type targetType, object data, IContext context);
    }
}
