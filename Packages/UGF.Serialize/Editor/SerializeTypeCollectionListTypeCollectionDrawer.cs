using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;

namespace UGF.Serialize.Editor
{
    internal class SerializeTypeCollectionListTypeCollectionDrawer : ReorderableListKeyAndValueDrawer
    {
        public SerializeTypeCollectionListTypeCollectionDrawer(SerializedProperty serializedProperty) : base(serializedProperty, "m_idValue", "m_type")
        {
        }
    }
}
