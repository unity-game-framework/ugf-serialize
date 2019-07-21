using System.Runtime.Serialization.Formatters.Binary;

namespace UGF.Serialize.Runtime.Formatter
{
    /// <summary>
    /// Provides utilities to work with formatter serializer.
    /// </summary>
    public static class SerializerFormatterUtility
    {
        /// <summary>
        /// Gets the binary serializer.
        /// </summary>
        public static ISerializer<byte[]> SerializerBinary { get; } = new SerializerFormatter(new BinaryFormatter());

        /// <summary>
        /// Gets the default name of the binary serializer used in <see cref="ISerializerProvider"/>.
        /// </summary>
        public static string SerializerBinaryName { get; } = "formatter-binary";
    }
}
