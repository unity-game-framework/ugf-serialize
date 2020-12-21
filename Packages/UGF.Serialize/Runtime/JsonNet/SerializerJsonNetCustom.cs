﻿#if UGF_SERIALIZE_JSONNET
using System;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UGF.JsonNet.Runtime;

namespace UGF.Serialize.Runtime.JsonNet
{
    public class SerializerJsonNetCustom : SerializerJsonNet
    {
        public SerializerJsonNetCustom(bool readable = false) : this(JsonNetUtility.DefaultSettings, readable)
        {
        }

        public SerializerJsonNetCustom(JsonSerializerSettings settings, bool readable = false) : base(settings, readable)
        {
        }

        protected virtual JsonSerializer OnCreateSerializer()
        {
            return JsonSerializer.Create(Settings);
        }

        protected virtual JsonWriter OnCreateWriter(object target)
        {
            return new SerializerJsonNetWriter(new StringWriter(CultureInfo.InvariantCulture));
        }

        protected virtual JsonReader OnCreateReader(Type targetType, string data)
        {
            return new JsonTextReader(new StringReader(data));
        }

        protected override string OnSerialize(object target)
        {
            JsonSerializer serializer = OnCreateSerializer();

            using JsonWriter writer = OnCreateWriter(target);

            serializer.Serialize(writer, target);

            return writer.ToString();
        }

        protected override object OnDeserialize(Type targetType, string data)
        {
            JsonSerializer serializer = OnCreateSerializer();

            using JsonReader reader = OnCreateReader(targetType, data);

            return serializer.Deserialize(reader, targetType);
        }
    }
}
#endif
