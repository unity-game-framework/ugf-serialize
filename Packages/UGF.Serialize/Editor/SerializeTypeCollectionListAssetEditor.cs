using System;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Serialize.Runtime;
using UnityEditor;
using UnityEngine;

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

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(m_listTypes.List.selectedIndices.Count == 0))
                {
                    if (GUILayout.Button("Refresh Selected"))
                    {
                        OnRefreshSelected();
                    }
                }

                if (GUILayout.Button("Refresh All"))
                {
                    OnRefreshAll();
                }

                EditorGUILayout.Space();
            }
        }

        private void OnRefreshSelected()
        {
            for (int i = 0; i < m_listTypes.List.selectedIndices.Count; i++)
            {
                int index = m_listTypes.List.selectedIndices[i];
                SerializedProperty propertyElement = m_listTypes.SerializedProperty.GetArrayElementAtIndex(index);

                if (!SerializeTypeDataEditorUtility.TryUpdate(propertyElement, out _, out Type type) && type != null)
                {
                    SerializeTypeDataEditorUtility.SetTypeData(propertyElement, type, Guid.NewGuid().ToString("N"));
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnRefreshAll()
        {
            SerializeTypeDataEditorUtility.Refresh(m_listTypes.SerializedProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
