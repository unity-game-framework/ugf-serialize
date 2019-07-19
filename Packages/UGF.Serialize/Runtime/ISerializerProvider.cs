using System.Collections.Generic;

namespace UGF.Serialize.Runtime
{
    public interface ISerializerProvider
    {
        ISerializer Default { get; }
        int DefaultMode { get; set; }
        IDictionary<int, ISerializer> Serializers { get; }

        int GetMode(ISerializer serializer);
        bool TryGetMode(ISerializer serializer, out int mode);
    }
}
