using System.Runtime.Serialization.Formatters.Binary;

namespace UGF.Serialize.Runtime.Formatter
{
    public static class SerializerFormatterUtility
    {
        public static ISerializer<byte[]> SerializerBinary { get; } = new SerializerFormatter(new BinaryFormatter());
        public static string SerializerBinaryName { get; } = "formatter-binary";
    }
}
