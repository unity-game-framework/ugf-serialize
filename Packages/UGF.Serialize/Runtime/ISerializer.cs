using System;

namespace UGF.Serialize.Runtime
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T target);
        byte[] Serialize(object target);
        T Deserialize<T>(byte[] data);
        object Deserialize(Type targetType, byte[] data);
    }
}
