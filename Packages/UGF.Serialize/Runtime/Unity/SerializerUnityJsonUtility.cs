using System.Text;

namespace UGF.Serialize.Runtime.Unity
{
    public static class SerializerUnityJsonUtility
    {
        public static ISerializer<string> SerializerTextCompact { get; } = new SerializerUnityJson(false);
        public static ISerializer<string> SerializerTextReadable { get; } = new SerializerUnityJson(true);
        public static ISerializer<byte[]> SerializerBytes { get; } = new SerializerUnityJsonBytes(Encoding.Default);
        public static string SerializerTextCompactName { get; } = "unity-json-text-compact";
        public static string SerializerTextReadableName { get; } = "unity-json-text-readable";
        public static string SerializerBytesName { get; } = "unity-json-bytes";
    }
}
