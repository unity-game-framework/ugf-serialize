#if UGF_SERIALIZE_JSONNET
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace UGF.Serialize.Runtime.Tests.JsonNet
{
    public class TestCustomSerializer
    {
        private class Target
        {
            public List<object> Targets { get; set; } = new List<object>();
        }

        private class Target1
        {
            public int IntValue { get; set; } = 10;
            public float FloatValue { get; set; } = 10.5F;
        }

        private class Target2
        {
            public bool BoolValue { get; set; } = true;
            public int IntValue { get; set; } = 10;
        }

        [Test]
        public void SerializeAndDeserialize()
        {
            var builder = Resources.Load<SerializerAsset>("SerializerJsonNetCustom");
            var serializer = builder.Build<ISerializer<string>>();

            var target = new Target()
            {
                Targets =
                {
                    new Target1(),
                    new Target2()
                }
            };

            string result = serializer.Serialize(target);
            string expected = Resources.Load<TextAsset>("SerializerJsonNetCustomResult").text;

            Assert.AreEqual(expected, $"{result}\r\n");

            var result2 = serializer.Deserialize<Target>(result);

            Assert.NotNull(result2);
            Assert.IsNotEmpty(result2.Targets);
            Assert.AreEqual(2, result2.Targets.Count);
            Assert.IsInstanceOf<Target1>(result2.Targets[0]);
            Assert.IsInstanceOf<Target2>(result2.Targets[1]);
            Assert.Pass(result);
        }
    }
}
#endif
