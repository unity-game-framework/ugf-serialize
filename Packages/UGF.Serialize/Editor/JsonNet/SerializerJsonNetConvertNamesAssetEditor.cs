#if UGF_SERIALIZE_JSONNET
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Serialize.Runtime.JsonNet;
using UnityEditor;

namespace UGF.Serialize.Editor.JsonNet
{
    [CustomEditor(typeof(SerializerJsonNetConvertNamesAsset), true)]
    internal class SerializerJsonNetConvertNamesAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyReadable;
        private SerializedProperty m_propertyIndent;
        private ReorderableListDrawer m_listSerializeNames;
        private ReorderableListDrawer m_listDeserializeNames;

        protected virtual void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyReadable = serializedObject.FindProperty("m_readable");
            m_propertyIndent = serializedObject.FindProperty("m_indent");
            m_listSerializeNames = new SerializerJsonNetConvertNamesListDrawer(serializedObject.FindProperty("m_serializeNames"));
            m_listDeserializeNames = new SerializerJsonNetConvertNamesListDrawer(serializedObject.FindProperty("m_deserializeNames"));

            m_listSerializeNames.Enable();
            m_listDeserializeNames.Enable();
        }

        protected virtual void OnDisable()
        {
            m_listSerializeNames.Disable();
            m_listDeserializeNames.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                OnDrawGUILayout();
            }
        }

        protected virtual void OnDrawGUILayout()
        {
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            EditorGUILayout.PropertyField(m_propertyReadable);
            EditorGUILayout.PropertyField(m_propertyIndent);

            m_listSerializeNames.DrawGUILayout();
            m_listDeserializeNames.DrawGUILayout();
        }
    }
}
#endif
