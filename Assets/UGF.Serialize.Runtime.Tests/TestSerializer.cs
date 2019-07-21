using System;
using NUnit.Framework;

namespace UGF.Serialize.Runtime.Tests
{
    public class TestSerializer
    {
        private class Target : SerializerBase<string>
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

        [Test]
        public void DataType()
        {
            var target = new Target();

            Assert.AreEqual(typeof(string), target.DataType);
        }
    }
}
