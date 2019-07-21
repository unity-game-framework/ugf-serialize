using System.Text;

namespace UGF.Serialize.Runtime.Unity
{
    /// <summary>
    /// Provides utilities to work with Unity Json serializer.
    /// </summary>
    public static class SerializerUnityJsonUtility
    {
        /// <summary>
        /// Gets the serializer that serialize to Json as text in compact layout.
        /// </summary>
        public static ISerializer<string> SerializerTextCompact { get; } = new SerializerUnityJson(false);

        /// <summary>
        /// Gets the serializer that serialize to Json as text in readable layout.
        /// </summary>
        public static ISerializer<string> SerializerTextReadable { get; } = new SerializerUnityJson(true);

        /// <summary>
        /// Gets the serializer that serialize to Json as byte array.
        /// </summary>
        public static ISerializer<byte[]> SerializerBytes { get; } = new SerializerUnityJsonBytes(Encoding.Default);

        /// <summary>
        /// Gets the default name of the Json compact text serializer used in <see cref="ISerializerProvider"/>.
        /// </summary>
        public static string SerializerTextCompactName { get; } = "unity-json-text-compact";

        /// <summary>
        /// Gets the default name of the Json readable text serializer used in <see cref="ISerializerProvider"/>.
        /// </summary>
        public static string SerializerTextReadableName { get; } = "unity-json-text-readable";

        /// <summary>
        /// Gets the default name of the Json byte array serializer used in <see cref="ISerializerProvider"/>.
        /// </summary>
        public static string SerializerBytesName { get; } = "unity-json-bytes";
    }
}
