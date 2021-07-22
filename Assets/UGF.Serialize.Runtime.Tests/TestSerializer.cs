using System;
using NUnit.Framework;

namespace UGF.Serialize.Runtime.Tests
{
    public class TestSerializer
    {
        private class Target : Serializer<string>
        {
            protected override object OnSerialize(object target)
            {
                return null;
            }

            protected override object OnDeserialize(Type targetType, string data)
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
