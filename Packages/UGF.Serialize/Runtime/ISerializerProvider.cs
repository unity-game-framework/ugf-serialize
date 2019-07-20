using System;
using System.Collections.Generic;

namespace UGF.Serialize.Runtime
{
    public interface ISerializerProvider
    {
        void Add(string name, ISerializer serializer);
        bool Remove<T>(string name);
        bool Remove(Type dataType, string name);
        void Clear();
        void Clear<T>();
        void Clear(Type dataType);
        ISerializer<T> Get<T>(string name);
        ISerializer Get(Type dataType, string name);
        bool TryGet<T>(string name, out ISerializer<T> serializer);
        bool TryGet(Type dataType, string name, out ISerializer serializer);
        IReadOnlyDictionary<string, ISerializer> GetSerializers(Type dataType);
        bool TryGetSerializers(Type dataType, out IReadOnlyDictionary<string, ISerializer> serializers);
        string GetName(ISerializer serializer);
        bool TryGetName(ISerializer serializer, out string name);
    }
}
