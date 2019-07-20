using System;
using System.Collections.Generic;

namespace UGF.Serialize.Runtime
{
    public interface ISerializer<TData> : ISerializer
    {
        new TData Serialize<T>(T target);
        new TData Serialize(object target);
        T Deserialize<T>(TData data);
        object Deserialize(Type targetType, TData data);
    }
}
