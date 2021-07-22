using System;
using NUnit.Framework;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime.Tests
{
    public class TestSerializer
    {
        private class Target : Serializer<string>
        {
            protected override object OnSerialize(object target, IContext context)
            {
                return null;
            }

            protected override object OnDeserialize(Type targetType, string data, IContext context)
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
