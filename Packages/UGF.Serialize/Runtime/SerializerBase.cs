using System;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerBase : ISerializer
    {
        public byte[] Serialize<T>(T target)
        {
            return Serialize((object)target);
        }

        public abstract byte[] Serialize(object target);

        public T Deserialize<T>(byte[] data)
        {
            return (T)Deserialize(typeof(T), data);
        }

        public abstract object Deserialize(Type targetType, byte[] data);
    }
}
