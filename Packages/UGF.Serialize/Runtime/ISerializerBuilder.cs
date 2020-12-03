using System;
using UGF.Builder.Runtime;

namespace UGF.Serialize.Runtime
{
    public interface ISerializerBuilder : IBuilder<ISerializer>
    {
        string Name { get; }
        Type DataType { get; }
    }
}
