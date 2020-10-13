using System;

namespace UGF.Serialize.Runtime
{
    public interface ISerializerBuilder
    {
        string Name { get; }
        Type DataType { get; }

        T Build<T>() where T : ISerializer;
        ISerializer Build();
    }
}
