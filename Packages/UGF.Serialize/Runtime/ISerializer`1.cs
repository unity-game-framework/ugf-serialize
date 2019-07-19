using System;

namespace UGF.Serialize.Runtime
{
    public interface ISerializer<TData>
    {
        TData Serialize<T>(T target);
        TData Serialize(object target);
        T Deserialize<T>(TData data);
        object Deserialize(Type targetType, TData data);
    }
}
