using System;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using UGF.Serialize.Runtime.Formatter;

namespace UGF.Serialize.Runtime.Tests.Formatter
{
    public class TestSerializerFormatter
    {
        [Serializable]
        private class Target
        {
            public bool BoolValue { get; set; } = true;
            public int IntValue { get; set; } = 10;
            public float FloatValue { get; set; } = 10.5F;
        }

        [Test]
        public void Serialize()
        {
            var serializer = new SerializerFormatter(new BinaryFormatter());
            var target = new Target();

            byte[] bytes = serializer.Serialize(target);

            Assert.NotNull(bytes);
            Assert.Greater(bytes.Length, 0);
        }

        [Test]
        public void Deserialize()
        {
            var serializer = new SerializerFormatter(new BinaryFormatter());
            var target = new Target();

            byte[] bytes = serializer.Serialize(target);
            var target0 = serializer.Deserialize<Target>(bytes);

            Assert.AreEqual(target.BoolValue, target0.BoolValue);
            Assert.AreEqual(target.IntValue, target0.IntValue);
            Assert.AreEqual(target.FloatValue, target0.FloatValue);
        }
    }
}
