#if UGF_SERIALIZE_JSONNET
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UGF.JsonNet.Runtime;
using UGF.JsonNet.Runtime.Converters;

namespace UGF.Serialize.Runtime.JsonNet
{
    public class SerializerJsonNetConvertNames : SerializerJsonNetCustom
    {
        public Dictionary<string, string> SerializeNames { get; } = new Dictionary<string, string>();
        public Dictionary<string, string> DeserializeNames { get; } = new Dictionary<string, string>();

        public SerializerJsonNetConvertNames(bool readable = false, int indent = 2) : base(readable, indent)
        {
        }

        public SerializerJsonNetConvertNames(JsonSerializerSettings settings, bool readable = false, int indent = 2) : base(settings, readable, indent)
        {
        }

        protected override JsonWriter OnCreateWriter(object target)
        {
            return new ConvertPropertyNameWriter(SerializeNames);
        }

        protected override JsonReader OnCreateReader(Type targetType, string data)
        {
            return new ConvertPropertyNameReader(DeserializeNames, data);
        }

        protected override string OnSerialize(object target)
        {
            string result = base.OnSerialize(target);

            if (Readable)
            {
                result = JsonNetUtility.Format(result);
            }

            return result;
        }
    }
}
#endif
