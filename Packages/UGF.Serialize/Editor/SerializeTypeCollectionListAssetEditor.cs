using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Serialize.Runtime;
using UnityEditor;

namespace UGF.Serialize.Editor
{
    [CustomEditor(typeof(SerializeTypeCollectionListAsset), true)]
    internal class SerializeTypeCollectionListAssetEditor : UnityEditor.Editor
    {
        private SerializeTypeDataCollectionDrawer m_listTypes;

        private void OnEnable()
        {
            m_listTypes = new SerializeTypeDataCollectionDrawer(serializedObject.FindProperty("m_types"));
            m_listTypes.Enable();
        }

        private void OnDisable()
        {
            m_listTypes.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listTypes.DrawGUILayout();
            }
        }
    }
}
