using System.Runtime.Serialization.Formatters.Binary;

namespace UGF.Serialize.Runtime.Formatter
{
    public class SerializerFormatterBinaryAsset : SerializerAsset<byte[]>
    {
        protected override ISerializer<byte[]> OnBuildTyped()
        {
            return new SerializerFormatter(new BinaryFormatter());
        }
    }
}
