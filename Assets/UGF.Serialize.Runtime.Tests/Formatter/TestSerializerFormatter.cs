using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.Serialize.Runtime.Formatter;
using UnityEngine.TestTools;

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
            var serializer = new SerializerFormatter();
            var target = new Target();

            byte[] bytes = serializer.Serialize(target, new Context());

            Assert.NotNull(bytes);
            Assert.Greater(bytes.Length, 0);
        }

        [Test]
        public void Deserialize()
        {
            var serializer = new SerializerFormatter();
            var target = new Target();

            byte[] bytes = serializer.Serialize(target, new Context());
            var target0 = serializer.Deserialize<Target>(bytes, new Context());

            Assert.AreEqual(target.BoolValue, target0.BoolValue);
            Assert.AreEqual(target.IntValue, target0.IntValue);
            Assert.AreEqual(target.FloatValue, target0.FloatValue);
        }

        [Test]
        public void DeserializeEmpty()
        {
            var serializer = new SerializerFormatter();

            var target = serializer.Deserialize<Target>(Array.Empty<byte>(), new Context());

            Assert.NotNull(target);
            Assert.IsInstanceOf<Target>(target);
        }

        [UnityTest]
        public IEnumerator SerializeAsync()
        {
            var serializer = new SerializerFormatter();
            var target = new Target();

            Task<byte[]> task = serializer.SerializeAsync(target, new Context());

            while (!task.IsCompleted)
            {
                yield return null;
            }

            byte[] bytes = task.Result;

            Assert.NotNull(bytes);
            Assert.Greater(bytes.Length, 0);
        }

        [UnityTest]
        public IEnumerator DeserializeAsync()
        {
            var serializer = new SerializerFormatter();
            var target = new Target();

            byte[] bytes = serializer.Serialize(target, new Context());

            Task<Target> task = serializer.DeserializeAsync<Target>(bytes, new Context());

            while (!task.IsCompleted)
            {
                yield return null;
            }

            Target target0 = task.Result;

            Assert.AreEqual(target.BoolValue, target0.BoolValue);
            Assert.AreEqual(target.IntValue, target0.IntValue);
            Assert.AreEqual(target.FloatValue, target0.FloatValue);
        }

        [Test]
        public void Copy()
        {
            var serializer = new SerializerFormatter();
            var target = new Target();

            Target copy = SerializeUtility.Copy(target, serializer, new Context());

            Assert.NotNull(copy);
            Assert.AreNotEqual(copy, target);
            Assert.AreNotSame(copy, target);
            Assert.AreEqual(copy.BoolValue, target.BoolValue);
            Assert.AreEqual(copy.IntValue, target.IntValue);
            Assert.AreEqual(copy.FloatValue, target.FloatValue);
        }
    }
}
