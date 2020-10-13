using System;
using UGF.Serialize.Editor.Unity;
using UGF.Serialize.Runtime;

namespace UGF.Serialize.Editor
{
    public static class SerializeEditorUtility
    {
        public static ISerializer<string> DefaultTextSerializer { get; } = new SerializerUnityJsonEditor(true);
        public static ISerializer<string> TextSerializer { get { return m_textSerializer; } set { m_textSerializer = value ?? throw new ArgumentException("Value can not be null."); } }

        private static ISerializer<string> m_textSerializer = DefaultTextSerializer;

        public static string ToText(object target)
        {
            return TextSerializer.Serialize(target);
        }

        public static T FromText<T>(string data)
        {
            return TextSerializer.Deserialize<T>(data);
        }

        public static object FromText(Type targetType, string data)
        {
            return TextSerializer.Deserialize(targetType, data);
        }
    }
}
