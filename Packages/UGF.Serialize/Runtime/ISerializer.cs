using System;

namespace UGF.Serialize.Runtime
{
    public interface ISerializer
    {
        Type DataType { get; }

        object Serialize<T>(T target);
        object Serialize(object target);
        T Deserialize<T>(object data);
        object Deserialize(Type targetType, object data);
    }
}
