using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UGF.Serialize.Runtime.Tests
{
    public class TestSerializerProvider
    {
        private class SerializerText : SerializerBase<string>
        {
            public override string Serialize(object target)
            {
                return null;
            }

            public override object Deserialize(Type targetType, string data)
            {
                return null;
            }
        }

        private class SerializeBytes : SerializerBase<byte[]>
        {
            public override byte[] Serialize(object target)
            {
                return null;
            }

            public override object Deserialize(Type targetType, byte[] data)
            {
                return null;
            }
        }

        [Test]
        public void Add()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            IReadOnlyDictionary<string, ISerializer> serializers0 = provider.GetSerializers<string>();
            IReadOnlyDictionary<string, ISerializer> serializers1 = provider.GetSerializers<byte[]>();

            Assert.NotNull(serializers0);
            Assert.NotNull(serializers1);
            Assert.AreEqual(1, serializers0.Count);
            Assert.AreEqual(1, serializers1.Count);
            Assert.Contains(text, serializers0.Values.ToArray());
            Assert.Contains(bytes, serializers1.Values.ToArray());
        }

        [Test]
        public void Remove()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            provider.Remove<string>("text");

            bool result0 = provider.TryGetSerializers<string>(out IReadOnlyDictionary<string, ISerializer> serializers0);

            IReadOnlyDictionary<string, ISerializer> serializers1 = provider.GetSerializers<byte[]>();

            Assert.False(result0);
            Assert.Null(serializers0);
            Assert.NotNull(serializers1);
            Assert.AreEqual(1, serializers1.Count);
            Assert.Contains(bytes, serializers1.Values.ToArray());
        }

        [Test]
        public void Clear()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            Assert.AreEqual(2, provider.DataTypesCount);

            provider.Clear();

            Assert.AreEqual(0, provider.DataTypesCount);
        }

        [Test]
        public void ClearByDataType()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("text2", text);
            provider.Add("bytes", bytes);

            Assert.AreEqual(2, provider.DataTypesCount);

            provider.Clear<string>();

            Assert.AreEqual(1, provider.DataTypesCount);
        }

        [Test]
        public void Get()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            ISerializer<string> serializer0 = provider.Get<string>("text");
            ISerializer<byte[]> serializer1 = provider.Get<byte[]>("bytes");

            Assert.NotNull(serializer0);
            Assert.NotNull(serializer1);
            Assert.AreEqual(text, serializer0);
            Assert.AreEqual(bytes, serializer1);
        }

        [Test]
        public void TryGet()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            bool result0 = provider.TryGet("text", out ISerializer<string> serializer0);
            bool result1 = provider.TryGet("bytes", out ISerializer<byte[]> serializer1);

            Assert.True(result0);
            Assert.True(result1);
            Assert.NotNull(serializer0);
            Assert.NotNull(serializer1);
            Assert.AreEqual(text, serializer0);
            Assert.AreEqual(bytes, serializer1);
        }

        [Test]
        public void GetSerializers()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            IReadOnlyDictionary<string, ISerializer> serializers0 = provider.GetSerializers<string>();
            IReadOnlyDictionary<string, ISerializer> serializers1 = provider.GetSerializers<byte[]>();

            Assert.NotNull(serializers0);
            Assert.NotNull(serializers1);
            Assert.AreEqual(1, serializers0.Count);
            Assert.AreEqual(1, serializers1.Count);
            Assert.Contains(text, serializers0.Values.ToArray());
            Assert.Contains(bytes, serializers1.Values.ToArray());
        }

        [Test]
        public void TryGetSerializers()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            bool result0 = provider.TryGetSerializers<string>(out IReadOnlyDictionary<string, ISerializer> serializers0);
            bool result1 = provider.TryGetSerializers<byte[]>(out IReadOnlyDictionary<string, ISerializer> serializers1);

            Assert.True(result0);
            Assert.True(result1);
            Assert.NotNull(serializers0);
            Assert.NotNull(serializers1);
            Assert.AreEqual(1, serializers0.Count);
            Assert.AreEqual(1, serializers1.Count);
            Assert.Contains(text, serializers0.Values.ToArray());
            Assert.Contains(bytes, serializers1.Values.ToArray());
        }

        [Test]
        public void GetName()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            string name0 = provider.GetName(text);
            string name1 = provider.GetName(bytes);

            Assert.AreEqual("text", name0);
            Assert.AreEqual("bytes", name1);
        }

        [Test]
        public void TryGetName()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            bool result0 = provider.TryGetName(text, out string name0);
            bool result1 = provider.TryGetName(bytes, out string name1);

            Assert.True(result0);
            Assert.True(result1);
            Assert.AreEqual("text", name0);
            Assert.AreEqual("bytes", name1);
        }

        [Test]
        public void GetDataTypes()
        {
            var provider = new SerializerProvider();
            var text = new SerializerText();
            var bytes = new SerializeBytes();

            provider.Add("text", text);
            provider.Add("bytes", bytes);

            IReadOnlyCollection<Type> types = provider.GetDataTypes();

            Assert.NotNull(types);
            Assert.AreEqual(2, types.Count);
            Assert.Contains(typeof(string), types.ToArray());
            Assert.Contains(typeof(byte[]), types.ToArray());
        }
    }
}
