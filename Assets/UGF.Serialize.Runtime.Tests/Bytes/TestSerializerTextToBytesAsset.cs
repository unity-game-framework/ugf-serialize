using NUnit.Framework;
using UGF.Serialize.Runtime.Bytes;
using UnityEngine;

namespace UGF.Serialize.Runtime.Tests.Bytes
{
    public class TestSerializerTextToBytesAsset
    {
        [Test]
        public void Build()
        {
            var asset = Resources.Load<SerializerAsset>("TestSerializerTextToBytes");
            ISerializer serializer = asset.Build();

            Assert.NotNull(serializer);
            Assert.IsInstanceOf<SerializerTextToBytes>(serializer);
        }
    }
}
