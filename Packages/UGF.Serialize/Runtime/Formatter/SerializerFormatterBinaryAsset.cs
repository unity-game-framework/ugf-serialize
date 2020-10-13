using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UGF.Serialize.Runtime.Formatter
{
    [CreateAssetMenu(menuName = "UGF/Serialize/SerializerFormatterBinary", order = 2000)]
    public class SerializerFormatterBinaryAsset : SerializerAsset<byte[]>
    {
        protected override ISerializer<byte[]> OnBuildTyped()
        {
            return new SerializerFormatter(new BinaryFormatter());
        }

        private void Reset()
        {
            Name = "formatter-binary";
        }
    }
}
