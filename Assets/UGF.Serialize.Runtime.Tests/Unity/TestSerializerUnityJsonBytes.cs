using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UGF.Serialize.Runtime.Unity;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Serialize.Runtime.Tests.Unity
{
    public class TestSerializerUnityJsonBytes
    {
        [Serializable]
        private class Target
        {
            [SerializeField] private bool m_boolValue = true;
            [SerializeField] private int m_intValue = 10;
            [SerializeField] private float m_floatValue = 10.5F;

            public bool BoolValue { get { return m_boolValue; } set { m_boolValue = value; } }
            public int IntValue { get { return m_intValue; } set { m_intValue = value; } }
            public float FloatValue { get { return m_floatValue; } set { m_floatValue = value; } }
        }

        [Test]
        public void Serialize()
        {
            var serializer = new SerializerUnityJsonBytes();
            var target = new Target();

            byte[] bytes = serializer.Serialize(target);

            Assert.NotNull(bytes);
            Assert.Greater(bytes.Length, 0);
        }

        [Test]
        public void Deserialize()
        {
            var serializer = new SerializerUnityJsonBytes();
            var target = new Target();

            byte[] bytes = serializer.Serialize(target);
            var target0 = serializer.Deserialize<Target>(bytes);

            Assert.AreEqual(target.BoolValue, target0.BoolValue);
            Assert.AreEqual(target.IntValue, target0.IntValue);
            Assert.AreEqual(target.FloatValue, target0.FloatValue);
        }

        [Test]
        public void DeserializeEmpty()
        {
            var serialize = new SerializerUnityJsonBytes();

            var target0 = serialize.Deserialize<Target>(Array.Empty<byte>());
            var target1 = serialize.Deserialize<Target>(Array.Empty<byte>());

            Assert.NotNull(target0);
            Assert.NotNull(target1);
            Assert.IsInstanceOf<Target>(target0);
            Assert.IsInstanceOf<Target>(target1);
        }

        [UnityTest]
        public IEnumerator SerializeAsync()
        {
            var serializer = new SerializerUnityJsonBytes();
            var target = new Target();

            Task<byte[]> task = serializer.SerializeAsync(target);

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
            var serializer = new SerializerUnityJsonBytes();
            var target = new Target();

            byte[] bytes = serializer.Serialize(target);

            Task<Target> task = serializer.DeserializeAsync<Target>(bytes);

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
            var serializer = new SerializerUnityJsonBytes();
            var target = new Target();

            Target copy = SerializeUtility.Copy(target, serializer);

            Assert.NotNull(copy);
            Assert.AreNotEqual(copy, target);
            Assert.AreNotSame(copy, target);
            Assert.AreEqual(copy.BoolValue, target.BoolValue);
            Assert.AreEqual(copy.IntValue, target.IntValue);
            Assert.AreEqual(copy.FloatValue, target.FloatValue);
        }
    }
}
