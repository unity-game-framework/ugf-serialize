using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;

namespace UGF.Serialize.Editor
{
    internal class SerializeTypeDataCollectionDrawer : ReorderableListKeyAndValueDrawer
    {
        public SerializeTypeDataCollectionDrawer(SerializedProperty serializedProperty) : base(serializedProperty, "m_idValue", "m_type")
        {
        }
    }
}
