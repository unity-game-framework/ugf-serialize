#if UGF_SERIALIZE_JSONNET
using UGF.EditorTools.Editor.IMGUI;
using UGF.Serialize.Runtime.JsonNet;
using UnityEditor;

namespace UGF.Serialize.Editor.JsonNet
{
    [CustomEditor(typeof(SerializerJsonNetConvertTypesAsset), true)]
    internal class SerializerJsonNetConvertTypesAssetEditor : SerializerJsonNetConvertNamesAssetEditor
    {
        private ReorderableListDrawer m_listTypes;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_listTypes = new ReorderableListDrawer(serializedObject.FindProperty("m_types"));

            m_listTypes.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            m_listTypes.Disable();
        }

        protected override void OnDrawGUILayout()
        {
            base.OnDrawGUILayout();

            m_listTypes.DrawGUILayout();
        }
    }
}
#endif
